using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayControl : AutoDestroy {

    public float speed = 0;
    public bool isRun = false;
    public int carriage = 1;

    private bool run = false;

    public void CreateCarriage()
    {
        GameObject body = transform.GetChild(1).gameObject;
        float bodyLength = body.GetComponent<MeshRenderer>().bounds.size.z;
        for (int i = 1; i < carriage; i++)
        {
            GameObject copy = Instantiate(body, transform);
            copy.transform.localPosition = new Vector3(0, 0, bodyLength * i);
        }
    }

    public void InitSubway(float speed = 10.0f, bool isRun = false, int cariage = 1)
    {
        this.speed = speed;
        this.isRun = isRun;
        this.carriage = cariage;

        CreateCarriage();
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        yield return new WaitUntil(() => Mathf.Abs(player.transform.position.z - transform.position.z) < 500);
        run = isRun;
    }

    private void Update()
    {
        if (run && isRun)
        {
            transform.position = transform.position - Vector3.forward * speed * Time.deltaTime;
        }

    }

}
