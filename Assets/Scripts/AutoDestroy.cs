using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

    protected Transform player;
    public float distanceDestroy = 100.0f;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => player.position.z > transform.position.z + distanceDestroy);
        Destroy(gameObject);
    }
}
