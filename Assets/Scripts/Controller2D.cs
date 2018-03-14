using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Set requiredComponents
[RequireComponent(typeof(BoxCollider2D))]

public class Controller2D : MonoBehaviour {

    // Variables
    const float skinWidth = .015f;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    // Highest angle the player can climb
    float maxClimbAngle=80;

    // Structs
    public struct collisionInfo
    {
        public bool above;
        public bool below;
        public bool left;
        public bool right;
        public bool climbingSlope;

        public float slopeAngle, slopeAngleOld;

        // Function to reset our collisionInfo struct variables
        public void Reset()
        {
            above = false;
            below = false;
            left = false;
            right = false;
            climbingSlope = false;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }

    // Create a struct to store our Raycast vectors(Collision detection rays)
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    // Collision detection mask for our player
    public LayerMask collisionMask;

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;

    // Reference to our collisionInfo struct
    public collisionInfo collisions;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();

        // Call our functions
        CalculateRaySpacing();
    }

    

    // Function to move our player
    public void Move(Vector3 velocity)
    {
        // Call our functions
        UpdateRaycastOrigins();
        collisions.Reset();

        // Only need to check for horizontal collisions if our x velocity is not equal to zero
        if(velocity.x!=0)
        {
            // Reference the velocity variable from our horizontalCollisions function 
            horizontalColissions(ref velocity);
        }

        // Only need to check for vertical collisions if our y velocity is not equal to zero
        if (velocity.y != 0)
        {
            // Reference the velocity variable from our VerticalCollisions function 
            verticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    // Function to draw our vertical collision detection rays 
    // and pass the variable as a reference to any other function that needs it
    void horizontalColissions(ref Vector3 velocity)
    {
        // If moving up directionY will be positive, if moving down directionY will be negative
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        // For loop to draw our vertical rays
        for (int i = 0; i < horizontalRayCount; i++)
        {
            // If moving down, set raycastOrigins to bottomLeft
            // otherwise, set raycastOrigins to topLeft
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);


            // Perform a raycast from our rayOrigin to the dire
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            // Draw our collision detection rays so we can see them for debugging
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            // Function to determine if the player is hit or not
            // i.e if the ray's cast by our player collide with something
            if (hit)
            {
                // Get the angle of surface we hit for climbing slopes
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                // Debug
                if(i==0 & slopeAngle <= maxClimbAngle)
                {
                    float distanceToSlopeStart = 0;
                    
                    // If we're trying to climb a new slope
                    if(slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        velocity.x -= distanceToSlopeStart * directionX;
                    }

                    climbSlope(ref velocity, slopeAngle);
                }

                if(!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;
            
                    // Fix problem with player jiggling on the slope
                    if(collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    // If the player has hit something and they're moving left, collisions.left is set to true
                    collisions.left = directionX == -1;

                    // If the player has hit something and they're moving right, collisions.right is set to true
                    collisions.right = directionX == 1;

                }


            }

          
        }
    }


    // Function to draw our vertical collision detection rays 
    // and pass the variable as a reference to any other function that needs it
    void verticalCollisions(ref Vector3 velocity)
    {
        // If moving up directionY will be positive, if moving down directionY will be negative
        float directionY = Mathf.Sign(velocity.y);

        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        // For loop to draw our vertical rays
        for (int i = 0; i < verticalRayCount; i++)
        {
            // If moving down, set raycastOrigins to bottomLeft
            // otherwise, set raycastOrigins to topLeft
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);


            // Perform a raycast from our rayOrigin to the dire
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            // Function to determine if the player is hit or not
            // i.e if the ray's cast by our player collide with something
            if(hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                // Re-calculate velocity.x
                if (collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                // If the player has hit something and they're moving down, collisions.below is set to true
                collisions.below = directionY == -1;

                // If the player has hit something and they're moving up, collisions.above is set to true
                collisions.above = directionY == 1;
            }

            // Draw our collision detection rays so we can see them for debugging
            Debug.DrawRay(rayOrigin, Vector2.up * directionY *rayLength, Color.red);
        }
    }

    // Create a method to update our rayCasts
    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;

        // Decrease the boundaries of the raycasts by (skinWidth*-2)
        bounds.Expand(skinWidth * -2);

        // Define the corners of our square (player) for collission detection
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    // Calculations for unit collision rays
    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;

        // Decrease the boundaries of the raycasts by (skinWidth*-2)
        bounds.Expand(skinWidth * -2);

        // Ensure there is a ray firing from each corner
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        // Calculate the spacing between each ray
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);

    }
    
    // Function for climbing slopes without having to jump
    void climbSlope(ref Vector3 velocity, float slopeAngle)
    {
        // Assign moveDistance to velocity.x in a positive value (Mathf.Abs)
        float moveDistance = Mathf.Abs(velocity.x);

        float climbVelocityY = Mathf.Sign(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        // Jumping on slope
        if (velocity.y >= climbVelocityY)
        {
            // Debug
            print("Jumping on slope");
        }
        else
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);

            // Enables player to jump when they are on the slope
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle; 
        }
    }
}
