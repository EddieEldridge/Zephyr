using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour {

    // Create a struct to store our Raycast vectors(Collision detection rays)
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
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
