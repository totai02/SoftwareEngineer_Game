using System.Collections;
using UnityEngine;

public class CamMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
