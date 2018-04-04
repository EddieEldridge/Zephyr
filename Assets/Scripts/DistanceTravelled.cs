using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Player))]
public class DistanceTravelled : MonoBehaviour
{
    Player player;

    public float distanceTravelled=0;
    public Vector3 lastPosition;

    public Text distanceText;


    void Start()
    {
        player = GetComponent<Player>();
        lastPosition = player.transform.position;
    }


    void Update()
    {
        lastPosition = player.transform.position;

        if (distanceText != null)
        {
            distanceTravelled += Vector3.Distance(player.transform.position, lastPosition);

            distanceText.text = "HIGH SCORE: " + player.transform.position.ToString(); // LINE WITH PROBLEM
        }

        else if(distanceText = null)
        {
            print("Yo shit is null");
        }
    }
}