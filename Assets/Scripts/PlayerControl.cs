﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 10;
    public float turnSpeed = 20;
    public float jumpForce = 500;

    public float gravityScale = 1.0f;

    public Transform groundCheck;
    public Transform frontCheck;

    public Vector3 destLane;

	public float stepWidth = 17;

    public bool isGrounded;

    private float globalGravity = -9.81f;

	public bool canMove = true;

	public Rigidbody rb;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        destLane = transform.position;
    }

    IEnumerator waitAction()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        canMove = true;
    }

    void GroundCheck()
    {
        float distance = Vector3.Distance(transform.position, groundCheck.position);
        isGrounded = Physics.Raycast(transform.position, groundCheck.position - transform.position, distance, 1 << 0);

        if (!isGrounded)
        {
            distance = Vector3.Distance(transform.position, frontCheck.position);
            isGrounded = Physics.Raycast(transform.position, frontCheck.position - transform.position, distance, 1 << 0);
        }

        if (isGrounded)
        {
            anim.SetBool("ground", true);
            anim.SetBool("fall", false);
        }
        else
        {
            anim.SetBool("ground", false);
        }
    }

    public void Die()
    {
        anim.SetBool("dead", true);
        speed = 0;
        Vector3 vel = rb.velocity;
        vel.y = -50;
        rb.velocity = vel;
        canMove = false;
        Invoke("RestartGame", 1.5f);
    }

    public void RestartGame()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        Application.LoadLevel("GamePlay");
#pragma warning restore CS0618 // Type or member is obsolete
    }

	public void PlayerMove()
    {
        Vector3 v = rb.velocity;
        v.z = speed;
        rb.velocity = v;

        if (!canMove) return;

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -1)
        {
            destLane = transform.position;
            destLane.x -= stepWidth;
            anim.SetBool("turn", true);
            anim.SetFloat("turnValue", 0.0f);
            canMove = false;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 1)
        {
            destLane = transform.position;
            destLane.x += stepWidth;
            anim.SetBool("turn", true);
            anim.SetFloat("turnValue", 1.0f);
            canMove = false;
            return;
        }
        else
        {
            Vector3 velocity = rb.velocity;
            velocity.x = 0;
            rb.velocity = velocity;
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("roll", true);
            if (!isGrounded) v.y = -50;
            rb.velocity = v;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce));
            isGrounded = false;
            canMove = false;
        }

        if (Input.GetKey(KeyCode.Escape)) Application.Quit();

        if (!canMove) StartCoroutine(waitAction());
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("roll", false);

        GroundCheck();

        PlayerMove();

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);

        if (!isGrounded && rb.velocity.y < 0)
        {
            anim.SetBool("fall", true);
        }

        if (transform.position.x != destLane.x)
        {
            float step = turnSpeed * Time.deltaTime;
            destLane.y = transform.position.y;
            destLane.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, destLane, step);
        }
        else if (anim.GetBool("turn"))
        {
            canMove = true;
            anim.SetBool("turn", false);
        }
    }
}
