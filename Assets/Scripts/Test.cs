using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using UnityEditor;

public class TestPlayerPossition
{

	[UnityTest]
	public IEnumerator TestPlayerPossitionJump()
	{
		// Use the Assert class to test conditions.
		// yield to skip a frame
		SetupScene();
		var player = GameObject.Find("Player");
		for (int i = 0; i < 10; i++)
		{
			yield return new WaitForSeconds(1);
			if (player.GetComponent<Rigidbody>().velocity.y > 40)
			{
				yield break;
			}
		
		}
	}

	[UnityTest]
	public IEnumerator TestJumb()
	{
		// Use the Assert class to test conditions.
		// yield to skip a frame
		SetupScene();
		var player = GameObject.Find("Player");
		var script = player.GetComponent<PlayerControl>();
		while(true)
		{
			//yield return new WaitForSeconds(1);
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
			//if (script.anim.GetBool("turn") == false)
			//{
			//	yield break;
			//}
			yield return new WaitUntil(()=>player.GetComponent<Rigidbody>().velocity.y > 0);
			yield return new WaitForSeconds(0.1f);
			if ( !script.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
			{
				Debug.Log("Jump fail");
				yield break;
			}
	
		}
	}

	[UnityTest]
	public IEnumerator TestFall()
	{
	// Use the Assert class to test conditions.
	// yield to skip a frame
		SetupScene();
		var player = GameObject.Find("Player");
		var script = player.GetComponent<PlayerControl>();
		while (true)
		{
			//yield return new WaitForSeconds(1);
		//	yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
			yield return new WaitUntil(() => player.GetComponent<Rigidbody>().velocity.y < 0 );
			yield return new WaitForSeconds(0.3f);
			if (!script.anim.GetCurrentAnimatorStateInfo(0).IsName("Fall") && script.isGrounded == false&&player.GetComponent<Rigidbody>().velocity.y < 0 )
			{
				Debug.Log("Fall fail");
				yield break;
			}
		}
	}


	void SetupScene()
	{
		//var scene = Resources.Load("Scenes/GamePlay");
		//GameObject player = (GameObject) MonoBehaviour.Instantiate(Resources.Load("Player"));
		//MonoBehaviour.Instantiate(Resources.Load("GameManager"));
		//MonoBehaviour.Instantiate(Resources.Load("Camera"));
		//MonoBehaviour.Instantiate(Resources.Load("Directional Light"));
		//MonoBehaviour.Instantiate(Resources.Load("Roads"));
		//MonoBehaviour.Instantiate(Resources.Load("Objects"));
		MonoBehaviour.Instantiate(Resources.Load("TestJumb"));
	}
}
