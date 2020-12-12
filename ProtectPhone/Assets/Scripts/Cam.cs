using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform targetObject;
    Vector3 offset = new Vector3(0, 0, 0);
    private Vector3 camPos;

    // Start is called before the first frame update

    void Start()
    {

    }

    void FixedUpdate()
    {
        camPos = targetObject.position + offset;
        transform.position = camPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
