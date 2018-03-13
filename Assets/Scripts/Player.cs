using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Set requiredComponents
[RequireComponent (typeof (Controller2D))]

public class Player : MonoBehaviour {

    // Variables
    float gravity = -20;
    float moveSpeed = 6;
    Vector3 velocity;

    // 2d controller
    Controller2D controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller2D>();
	}

    void Update()
    {
        // Setup horizontal unit collision
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Every frame, set the x velocity
        velocity.x = input.x * moveSpeed;

        // Set the velocity for our player
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
