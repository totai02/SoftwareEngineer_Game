using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSubway : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().die();
        }
    }
}
