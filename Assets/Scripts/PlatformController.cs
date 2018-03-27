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

    // Lists
    List <passengerMovement> passengerMovement;

    // Structs 
    struct passengerMovement
    {   
        // Variables
        public Transform transform;
        public Vector3 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform)
        {
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Call our functions
        UpdateRaycastOrigins();

        // Smoothly moves around our platform when we change its x and y values in the Unity inspector
        Vector3 velocity = move * Time.deltaTime;

        CalculatePassengerMovement(velocity);

         // Set movePassengers to be true before we move the player
        MovePassengers(true);

        transform.Translate(velocity);

        // Set movePassengers to be true before we move the player
        MovePassengers(false);
    }

    // Function to control movement of 'passengers' (i.e players standing on the moving platform)
    void CalculatePassengerMovement(Vector3 velocity)
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

        // Horizontally moving platform
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;

            // For loop to draw our vertical rays
            for (int i = 0; i < horizontalRayCount; i++)
            {
                // If moving down, set raycastOrigins to bottomLeft
                // otherwise, set raycastOrigins to topLeft
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);


                // Perform a raycast from our rayOrigin to the dire
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

                // Draw our collision detection rays so we can see them for debugging
                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

                // If hit (i.e there's a passenger on the platform)
                if (hit)
                {
                    // If the passenger isn't in the hashSet of players that have already mvoed this frame
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        // Add the passenger to the hashSet 'movedPassengers'
                        movedPassengers.Add(hit.transform);

                        // Variables to move our passenger with our platform
                        float pushY = 0;
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;

                        // Move the player
                        hit.transform.Translate(new Vector3(pushX, pushY));

                    }

                }
            }
        }

        // Passenger on top of horizontally or downward moving platform
        if (directionY == -1 || velocity.y == 0 && velocity.x != 0)
        {
            float rayLength = skinWidth * 2;

            // For loop to draw our vertical rays
            for (int i = 0; i < verticalRayCount; i++)
            {
                // If moving down, set raycastOrigins to bottomLeft
                // otherwise, set raycastOrigins to topLeft
                Vector2 rayOrigin =raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
      
                // Perform a raycast from our rayOrigin to the dire
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                // If hit (i.e there's a passenger on the platform)
                if (hit)
                {
                    // If the passenger isn't in the hashSet of players that have already mvoed this frame
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        // Add the passenger to the hashSet 'movedPassengers'
                        movedPassengers.Add(hit.transform);

                        // Variables to move our passenger with our platform
                        float pushY = velocity.y;
                        float pushX = velocity.x;

                        // Move the player
                        hit.transform.Translate(new Vector3(pushX, pushY));

                    }

                }

            }
        }
    }

    void MovePassengers(bool beforeMovePlatform)   
    {
       


    }

}
        
