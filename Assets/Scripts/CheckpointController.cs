using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour {

    // Variables
    public bool checkpointReached;
    public int nextLevel;
    public Text bestTimeText;

    // Timer variables
    public float timer = 0;
    public Text timerText;

    public void Update()
    { 
        //Time.deltaTime will increase the value with 1 every second.
        timer += Time.deltaTime;

        if (timerText != null)
        {
            // Set the text
            timerText.text = "CURRENT TIME: " + timer.ToString();

        }



        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        // Tutorial Level
        if (sceneName == "Tutorial")
        {

            if (bestTimeText != null)
            {
                bestTimeText.text = "BEST TIME: " + PlayerPrefs.GetFloat("BestTutorialTime").ToString();
            }
        }
        // Level1
        else if (sceneName == "Level1")
        {
            if (bestTimeText != null)
            {
                bestTimeText.text = "BEST TIME: " + PlayerPrefs.GetFloat("BestLevel1Time").ToString();
            }
        }

        // Level2
        else if (sceneName == "Level2")
        {
            if (bestTimeText != null)
            {
                bestTimeText.text = "BEST TIME: " + PlayerPrefs.GetFloat("BestLevel2time", 0).ToString();
            }
        }

        // Level3
        else if (sceneName == "Level3")
        {
            if (bestTimeText != null)
            {
                bestTimeText.text = "BEST TIME: " + PlayerPrefs.GetFloat("BestLevel3Time", 0).ToString();
            }
        }
    }
    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        print("Best Tutoril Time:" + PlayerPrefs.GetFloat("BestTutorialTime").ToString());

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (other.tag == "Player")
        {
            print("COLLISIONS function");

                // Tutorial Level
                if (sceneName == "Tutorial")
                {
                    print("INSIDE function");
                    PlayerPrefs.SetFloat("BestTutorialTime", timer);                 
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
                else if (sceneName == "Level2")
                {
                    // If statement to set our best time
                    if (timer < PlayerPrefs.GetFloat("BestLevel2time"))
                    {
                        PlayerPrefs.SetFloat("BestLevel2time", timer);
                    }
                }

                // Level3
                else if (sceneName == "Level3")
                {
                    // If statement to set our best time
                    if (timer < PlayerPrefs.GetFloat("BestLevel3Time"))
                    {
                        PlayerPrefs.SetFloat("BestLevel3Time", timer);
                    }
                }
            

            // Increment our scene forward 1 scene from the scene we are on 
            // Apply our fadeEffect
            float fadeTime = GameObject.Find("BackgroundImage").GetComponent<FadeEffect>().BeginFade(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            checkpointReached = true;
        }
    }

}
