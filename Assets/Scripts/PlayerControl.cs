using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public int speed = 10;
    public float jumpForce = 10.0f;

    private bool grounded;
    private int laneNum = 3;
    private int currentLane = 2;
    private bool isMoving = false;

    private Rigidbody rb;
    private Animator anim;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!grounded)
        {
            anim.SetBool("isGrounded", true);
        }
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    public void die()
    {
        anim.SetTrigger("isDead");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 velocity = rb.velocity;
            velocity.x = -speed;
            rb.velocity = velocity;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 velocity = rb.velocity;
            velocity.x = speed;
            rb.velocity = velocity;
        }
        else
        {
            Vector3 velocity = rb.velocity;
            velocity.z = 0;
            rb.velocity = velocity;
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector3(0, jumpForce));
            anim.SetBool("isGrounded", false);
            grounded = false;
        }
    }
}
