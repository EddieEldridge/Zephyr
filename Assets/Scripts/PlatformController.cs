using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RaycastController
{

    // Layer masks
    public LayerMask passengerMask;

    // Vector to move our platform
    public Vector3 move;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

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
        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;

            // For loop to draw our vertical rays
            for (int i = 0; i < verticalRayCount; i++)
            {
                // If moving down, set raycastOrigins to bottomLeft
                // otherwise, set raycastOrigins to topLeft
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);


                // Perform a raycast from our rayOrigin to the dire
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

                // If hit (i.e there's a passenger on the platform)
                if (hit)
                {
                    // Variables to move our passenger with our platform
                    float pushY = velocity.y - (hit.distance - skinWidth) * directionY;
                    float pushX = (directionY == 1) ? velocity.x : 0;

                    // Move the player
                    hit.transform.Translate(new Vector3(pushX, pushY));

                }

            }
        }
    }

}
        
