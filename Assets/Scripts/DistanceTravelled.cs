﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Player))]
public class DistanceTravelled : MonoBehaviour
{
    
    // Distance variables
    public float distanceTravelled;
    public float startPos;
    public float currentPos;
    public int multiplier = 2;
    public GameObject player;
    public Text distanceText;

    void Start()
    {
        startPos = player.transform.position.x;
    }


    void Update()
    {
        currentPos = player.transform.position.x - startPos;

        int distanceTravelled = Mathf.Abs(Mathf.RoundToInt(currentPos * multiplier));

        if (distanceText != null)
        {
            distanceText.text = "DISTANCE TRAVELLED: " + distanceTravelled.ToString(); // LINE WITH PROBLEM
        }
    }
}