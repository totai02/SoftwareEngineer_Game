using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{

    public Transform target;

    public float smooth = 0.6f;

    private Vector3 offset;


    // Use this for initialization
    void Start()
    {
        offset = this.target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, target.position - offset, smooth * Time.deltaTime * 10);
        newPos.z = (target.position - offset).z;
        transform.position = newPos;
    }
}

