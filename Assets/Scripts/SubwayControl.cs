using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayControl : ObjectInGame {
    public float speed = 0;
    public bool isRun = false;
    public int carriage = 1;

    public void Start()
    {
        initSubway(speed, isRun, carriage); 
    }

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

    public void initSubway(float speed = 10.0f,bool isRun = false, int cariage = 1)
    {
        this.speed = speed;
        this.isRun = isRun;
        this.carriage = cariage;

        CreateCarriage();
    }

    private void Update()
    {
        if (isRun)
        {
            transform.position = transform.position - Vector3.forward * speed * Time.deltaTime;
        }
    }

}
