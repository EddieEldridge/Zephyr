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
        // Call our functions
        UpdateRaycastOrigins();

        // Smoothly moves around our platform when we change its x and y values in the Unity inspector
        Vector3 velocity = move * Time.deltaTime;

        MovePassengers(velocity);

        transform.Translate(velocity);
    }

    // Function to control movement of 'passengers' (i.e players standing on the moving platform)
    void MovePassengers(Vector3 velocity)
    {
        // Create hashSet of passengers that have already moved this frame to prevent weird issues occuring if there are multiple passengers on one platform
        // We use a hashSet as they are fast to add to and fast to check if certain things are contained withing them
        HashSet<Transform> movedPassengers = new HashSet<Transform>();

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
                    // If the passenger isn't in the hashSet of players that have already mvoed this frame
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        // Add the passenger to the hashSet 'movedPassengers'
                        movedPassengers.Add(hit.transform);

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

}
        
