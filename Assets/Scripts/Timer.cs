using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Timer : MonoBehaviour {

    // Timer variables
    public float timer = 0;
    public Text timerText;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update ()
    {
       // checkpointReached2 = CheckpointController.setCheckpoint(checkpointReached);
        timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.

        if (timerText != null)
        {
            // Set the text
            timerText.text = "CURRENT TIME: " + timer.ToString();       
            
        }

        
    }
}
