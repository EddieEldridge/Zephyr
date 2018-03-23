using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Function to move our camera with our target 
    void LateUpdate()
    {
        // Set our camera to focus on our target (the player)
        transform.position = target.position + offset;
    }
}
