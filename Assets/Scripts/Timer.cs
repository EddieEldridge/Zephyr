using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Timer : MonoBehaviour {

    // Timer variables
    public float timer = 0;
    public Text timerText;
    public Text bestTimeText;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.

        if (timerText != null)
        {
            timerText.text = "CURRENT TIME: " + timer.ToString();

            // Create a temporary reference to the current scene.
            Scene currentScene = SceneManager.GetActiveScene();

            // Retrieve the name of this scene.
            string sceneName = currentScene.name;

            // Tutorial Level
            if (sceneName == "Tutorial")
            {
                // If statement to set our best time
                if (timer < PlayerPrefs.GetFloat("BestTutorialTime"))
                {

                    PlayerPrefs.SetFloat("BestTutorialTime", timer);

                }
            }

            // Level1
            else if (sceneName == "Level1")
            {
                // If statement to set our best time
                if (timer < PlayerPrefs.GetFloat("BestLevel1Time"))
                {
                    PlayerPrefs.SetFloat("BestLevel1Time", timer);
                }
            }

            // Level2
            else if (sceneName == "Level1")
            {
                // If statement to set our best time
                if (timer < PlayerPrefs.GetFloat("BestLevel1Time"))
                {
                    PlayerPrefs.SetFloat("BestLevel1Time", timer);
                }
            }
        }

        if(bestTimeText!=null)
        {

        }
    }
}
