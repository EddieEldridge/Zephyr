using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTravelled : MonoBehaviour 
{

	// Variables
	private Vector3 startPosition;
	public Text scoreText;
	public int score;
	public int highScore;
   	
	// Functions
    void Awake()
	 {
        startPosition = transform.position;
        highScore = 0;
    }

	void Start ()
	{
		displayText();
	}
 
    void Update() 
	{
        score = Mathf.RoundToInt(Mathf.Abs(transform.position.x - startPosition.x));
        highScore = Mathf.Max(score, highScore);
		score++;
    }
 
	void displayText() 
	{
		scoreText.text = "Distance Travelled: " + score.ToString();
	}

}
