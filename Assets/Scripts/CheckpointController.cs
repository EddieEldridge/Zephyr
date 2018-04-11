using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour {

    // Variables
    public bool checkpointReached;
    public int nextLevel;
    public float timer = 3000;
    public Text bestTimeText;


    private void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        PlayerPrefs.SetFloat("BestTutorialTime", 100);


        // Tutorial Level
        if (sceneName == "Tutorial")
        {
            bestTimeText.text = "BEST TIME: " + PlayerPrefs.GetFloat("BestTutorialTime", 0).ToString();
        }

        // Level1
        else if (sceneName == "Level1")
        {
            bestTimeText.text = "BEST TIME: " + PlayerPrefs.GetFloat("BestLevel1Time", 0).ToString();
        }

        // Level2
        else if (sceneName == "Level2")
        {
            bestTimeText.text = "BEST TIME: " + PlayerPrefs.GetFloat("BestLevel2time", 0).ToString();
        }

        // Level3
        else if (sceneName == "Level3")
        {
            bestTimeText.text = "BEST TIME: " + PlayerPrefs.GetFloat("BestLevel3Time", 0).ToString();
        }
    }
    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (other.tag == "Player")
        {

            // Apply our fadeEffect
            float fadeTime = GameObject.Find("BackgroundImage").GetComponent<FadeEffect>().BeginFade(1);       

            // Set time as best time
            if (bestTimeText != null)
            {

                // Tutorial Level
                if (sceneName == "Tutorial")
                { 
                    // If statement to set our best time
                    if (timer > PlayerPrefs.GetFloat("BestTutorialTime"))
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
            }


            // Increment our scene forward 1 scene from the scene we are on 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            checkpointReached = true;
        }
    }

}
