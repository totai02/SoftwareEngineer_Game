using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : AutoDestroy {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().Die();
    }
}
