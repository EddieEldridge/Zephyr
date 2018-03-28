using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Text;

public class DistanceTravelled : MonoBehaviour 
{

	// Variables
	private Vector3 startPosition;

	// Setters and Getters
	public int score 
	{
        get;
        private set;
    }
 
    public int bestScore
	{
        get;
        private set;
    }
   	
	// Functions
    void Awake()
	 {
        startPosition = transform.position;
        bestScore = 0;
    }
 
    void Update() 
	{
        score = Mathf.RoundToInt(Mathf.Abs(transform.position.x - startPosition.x));
        bestScore = Mathf.Max(score, bestScore);
    }
 
    public void OnGameOver() 
	{
        transform.position = startPosition;
    }

	void displayText() 
	{
		myText.text = "This is my sample text";
	}

}
