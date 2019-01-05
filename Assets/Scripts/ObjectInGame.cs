using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInGame : MonoBehaviour
{

    public Transform player;
    public float distance = 70f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => player.position.z > transform.position.z + distance);
        Destroy(gameObject);
    }
}
