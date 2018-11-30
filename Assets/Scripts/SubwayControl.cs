using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayControl : MonoBehaviour {
    public float speed = 0;
    public bool isRun = false;
    public int cariage = 1;

    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        initSubway(speed, isRun, cariage); 
    }

    private void createCariage()
    { 
        GameObject body = transform.GetChild(1).gameObject;
        float bodyLength = body.GetComponent<MeshRenderer>().bounds.size.z;
        for (int i = 1; i < cariage; i++)
        {
            GameObject copy = Instantiate(body, transform);
            copy.transform.localPosition = new Vector3(0, 2.5f, bodyLength * i);
        }
    }

    public void initSubway(float speed = 10.0f,bool isRun = false, int cariage = 1)
    {
        this.speed = speed;
        this.isRun = isRun;
        this.cariage = cariage;

        createCariage();
    }

    private void Update()
    {
        if (isRun)
        {
            transform.position = transform.position - Vector3.forward * speed * Time.deltaTime;
        }
    }

}
