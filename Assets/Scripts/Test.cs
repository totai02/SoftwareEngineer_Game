using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using UnityEditor;

public class TestPlayerPossition
{

    [UnityTest]
    public IEnumerator TestPlayerPositionJump()
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
    public IEnumerator TestJump()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        SetupScene();
        var player = GameObject.Find("Player");
        var script = player.GetComponent<PlayerControl>();
        while (true)
        {
            //yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            //if (script.anim.GetBool("turn") == false)
            //{
            //	yield break;
            //}
            yield return new WaitUntil(() => player.GetComponent<Rigidbody>().velocity.y > 0);
            yield return new WaitForSeconds(0.1f);
            if (!script.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
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
            yield return new WaitUntil(() => player.GetComponent<Rigidbody>().velocity.y < 0);
            yield return new WaitForSeconds(0.3f);
            if (!script.anim.GetCurrentAnimatorStateInfo(0).IsName("Fall") && script.isGrounded == false && player.GetComponent<Rigidbody>().velocity.y < 0)
            {
                yield break;
            }
        }
    }


    void SetupScene()
    {
        MonoBehaviour.Instantiate(Resources.Load("TestJumb"));
    }
}
