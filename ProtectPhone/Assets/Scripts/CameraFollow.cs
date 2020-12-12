using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    Vector3 directionOffset;

    void shootBegin(){
        //todo
    }

    void shootEnd(){
        //todo
    }
    // Start is called before the first frame update
    void Start()
    {
        directionOffset = new Vector3(5, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(transform.position);
        Vector3 tragetPos = target.position + System.Math.Sign(target.localScale.x) * directionOffset;
        transform.position = Vector3.Lerp(transform.position, tragetPos, smoothing * Time.deltaTime);

    }
}
