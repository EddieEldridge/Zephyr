using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Set requiredComponents
[RequireComponent (typeof (Controller2D))]

public class Player : MonoBehaviour {

    // Variables
    float gravity = -20;
    Vector3 velocity;

    // 2d controller
    Controller2D controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller2D>();
	}

    private void Update()
    {
        // Set the velocity for our player
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
