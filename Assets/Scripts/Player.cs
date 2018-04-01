using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Set requiredComponents
[RequireComponent (typeof (Controller2D))]

public class Player : MonoBehaviour {

    // Variables
    public float jumpHeight =4;
    public float timeToJumpApex =.4f;

    public float wallslideSpeedMax = 3;

    float moveSpeed = 6;

    float accelerationTimeAirborne =.2f;
    float accelerationTimeGrounded =.1f;

    float gravity;
    float jumpVelocity;
    float velocityXSmoothing;
    Vector3 velocity;

    // 2d controller
    Controller2D controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller2D>();

        // Calculations for our gravity
        gravity = -(2 * jumpHeight) /Mathf.Pow (timeToJumpApex, 2);

        // Calculations for our jumpVelocity
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        // Wall sliding
        bool wallSliding = false;

        // Depending on what's around the player do the following
        if(controller.collisions.left || controller.collisions.right && !controller.collisions.below && velocity.y <0)
        {
            wallSliding = true;

            if(velocity.y < -wallslideSpeedMax)
            {
                velocity.y = -wallslideSpeedMax;
            }
        }

        // Prevent gravity from accumulating if the player is resting on a surface 
        if(controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        // Setup horizontal unit collision
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Jumping!
        // If the player presses space, and there is a collision occuring below them (i.e they are standing on something)
        if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        // Smooth out animations
        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded: accelerationTimeAirborne);

        // Every frame, set the x velocity
       // velocity.x = input.x * moveSpeed;

        // Set the velocity for our player
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
