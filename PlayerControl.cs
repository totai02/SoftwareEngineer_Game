using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public KeyCode MoveL;
	public KeyCode MoveR;
	public float horizVel = 0;
	public int laneNum=2;
	public bool moveLocked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody> ().velocity = new Vector3(horizVel, 0, 4);
		if ((Input.GetKeyDown(MoveL)) && (laneNum > 1) && (moveLocked == false))
		{
			horizVel = -2;
			StartCoroutine(StopSlide());
			laneNum -= 1;
			moveLocked = true;
		}
		if ((Input.GetKeyDown(MoveR)) && (laneNum < 3) && (moveLocked == false))
		{
			horizVel = 2;
            StartCoroutine(StopSlide());
			laneNum += 1;
			moveLocked = true;
		}
	}
	IEnumerator StopSlide() {
		yield return new WaitForSeconds(.5f);
		horizVel = 0;
		moveLocked = false;
	}
}
