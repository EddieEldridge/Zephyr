using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RaycastController {

    // Vector to move our platform
    public Vector3 move;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {

        // Smoothly moves around our platform when we change its x and y values in the Unity inspector
        Vector3 velocity = move * Time.deltaTime;
        transform.Translate(velocity);
    }

    // Function to control movement of 'passengers' (i.e players standing on the moving platform)
    void MovePassengers(Vector3 velocity)
    {
        // Variables
        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        // Vertically moving platform
        if(velocity.y !=0)
        {

        }
    }
}
