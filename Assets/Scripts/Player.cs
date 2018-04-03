using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Set requiredComponents
[RequireComponent (typeof (Controller2D))]

public class Player : MonoBehaviour {

    // Variables
    public float jumpHeight =4;
    public float timeToJumpApex =.4f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallslideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    float moveSpeed = 10;

    float accelerationTimeAirborne =.2f;
    float accelerationTimeGrounded =.1f;

    float gravity;
    float jumpVelocity;
    float velocityXSmoothing;
    Vector3 velocity;

    // 2D controller
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
        // Setup horizontal unit collision
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        int wallDirX = (controller.collisions.left) ? -1 : 1;

        // Smooth out animations
        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        // Wall sliding
        bool wallSliding = false;

        // Depending on what's around the player do the following
        if(controller.collisions.left || controller.collisions.right && !controller.collisions.below && velocity.y <0)
        {
            wallSliding = true;

            // Set our downward velocity when on a wall
            if (velocity.y < -wallslideSpeedMax)
            {
                velocity.y = -wallslideSpeedMax;
            }

            // Smoothen up wall jumping from left to right and vice versa
            if(timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if(input.x != wallDirX && input.x !=0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }

                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }

            // In the case of timeToWallUnstick being less than 0
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }

        // Prevent gravity from accumulating if the player is resting on a surface 
        if(controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

       
        // Jumping!
        // If the player presses space, and there is a collision occuring below them (i.e they are standing on something)
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Wall jumping
            if(wallSliding)
            {
                if (wallDirX == input.x)
                {
                    // Move away from wall
                    velocity.x = -wallDirX * wallJumpClimb.x;

                    velocity.y = wallJumpClimb.y;
                }

                else if (input.x == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;

                    velocity.y = wallJumpOff.y;
                }

                else
                {
                    velocity.x = -wallDirX * wallLeap.x;

                    velocity.y = wallLeap.y;
                }
            }

            // Normal jump
            if(controller.collisions.below)
            {
                velocity.y = jumpVelocity;
            }
           
        }


        // Set the velocity for our player
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
