using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RaycastController
{

    // Layer masks
    public LayerMask passengerMask;

    // Dictionary to reduced the amount of getComponent calls which consume processing power
    Dictionary<Transform,Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D>();

    // Variables for our platform movement calculations
    public float speed;
    public bool cyclic;
    int fromWayPointIndex;
    float percentBetweenWaypoints;

    // Lists
    List <PassengerMovement> passengerMovement;

    // Arrays
    public Vector3[] localWaypoints;
    Vector3[] globalWaypoints;

    // Structs 
    struct PassengerMovement
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

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        globalWaypoints = new Vector3[localWaypoints.Length];

        for (int i=0; i<localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
    }



    // Update is called once per frame
    void Update()
    {
        // Call our functions
        UpdateRaycastOrigins();

        // Smoothly moves around our platform when we change its x and y values in the Unity inspector
        Vector3 velocity = CalculatePlatformMovement();

        CalculatePassengerMovement(velocity);

         // Set movePassengers to be true before we move the player
        MovePassengers(true);

        transform.Translate(velocity);

        // Set movePassengers to be true before we move the player
        MovePassengers(false);
    }

    // Function to calculate the movement of the moving platforms and return it to our velocity above
    Vector3 CalculatePlatformMovement()
    {
        fromWayPointIndex %= globalWaypoints.Length;

        int toWayPointIndex = (fromWayPointIndex + 1) % globalWaypoints.Length;

        // Get the distance between the two waypoints
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWayPointIndex], globalWaypoints[fromWayPointIndex]);

        // Further away the waypoints are the faster the platform will move
        // i.e relative speed based on distance
        percentBetweenWaypoints += Time.deltaTime * (speed/distanceBetweenWaypoints);

        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWayPointIndex], globalWaypoints[toWayPointIndex], percentBetweenWaypoints);

        if(percentBetweenWaypoints >=1)
        {
            percentBetweenWaypoints = 0;
            fromWayPointIndex++;
           
            if(!cyclic)
            {
                // When we reach the end of the wayPoints array 
                if (fromWayPointIndex >= globalWaypoints.Length - 1)
                {
                    fromWayPointIndex = 0;

                    // Reverse our array to send our platform back in the opposite direction
                    System.Array.Reverse(globalWaypoints);
                }
            }
        
        }

        return newPos - transform.position;
    }

    // Function to control movement of 'passengers' (i.e players standing on the moving platform)
    void CalculatePassengerMovement(Vector3 velocity)
    {   
        // Create hashSet of passengers that have already moved this frame to prevent weird issues occuring if there are multiple passengers on one platform
        // We use a hashSet as they are fast to add to and fast to check if certain things are contained withing them
        HashSet<Transform> movedPassengers = new HashSet<Transform>();

        // Create a new instance of our passengerMovement list
        passengerMovement = new List<PassengerMovement>();

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

                        // Add to our passengerMovement list
                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY==1, true));
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
                        float pushY = -skinWidth;
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;

                           // Add to our passengerMovement list
                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true));

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

                        // Add to our passengerMovement list
                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));

                    }

                }

            }
        }
    }

    void MovePassengers(bool beforeMovePlatform)   
    {
        
        foreach(PassengerMovement passenger in passengerMovement)
       {    
           // Check to see if our passenger is contained in our dictionary
           if(!passengerDictionary.ContainsKey(passenger.transform))
           {
               passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<Controller2D>());
           }

           if(passenger.moveBeforePlatform == beforeMovePlatform)
           {
               passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
           }
       }

    }

    // Function to draw our waypoints for our moving platforms
    private void OnDrawGizmos()
    {   
        if(localWaypoints !=null)
        {
            // Set the color of our Gizmos to be red
            Gizmos.color = Color.red;

            // Define the size of our gizmos
            float size = .3f;

            // Loop through all of our waypoints
            for(int i=0; i < localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;

                // Draw a cross for our waypoints
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);

            }
        }
    }
}
        
