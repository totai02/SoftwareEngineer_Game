using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : AutoDestroy {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().Die();
            if (transform.parent.GetComponent<SubwayControl>())
            {
                transform.parent.GetComponent<SubwayControl>().isRun = false;
            }
        }
    }
}
