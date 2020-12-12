using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playertrack;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playertrack.position.x , playertrack.position.y, -10f);
    }
}
