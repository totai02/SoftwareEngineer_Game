using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Transform target;
    public float smooth = 0.5f;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = target.position - transform.position;	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, target.position - offset, smooth);
        Vector3 pos = transform.position;
        pos.z = (target.position - offset).z;
        transform.position = pos;
    }
}
