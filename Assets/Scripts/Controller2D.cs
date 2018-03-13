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

    public LayerMask colissionMask;

    // Create a struct to store our Raycast vectors(Collision detection rays)
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;

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

        // Reference the velocity variable from our VerticalCollisions function 
        verticalCollisions(ref velocity);

        transform.Translate(velocity);
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
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, colissionMask);

            // Function to determine if the player is hit or not
            // i.e if the ray's cast by our player collide with something
            if(hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
            }

            Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);
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
    
    
}
