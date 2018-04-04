using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

    // Timer variables
    public float timer = 0;
    public Text timerText;


    // Use this for initialization
    void Start ()
    {
        timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.
    }

    // Update is called once per frame
    void Update ()
    {
        if (timerText != null)
        {
            timerText.text = "TIME: " + timer.ToString(); // LINE WITH PROBLEM
        }
    }
}
