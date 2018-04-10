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
    }

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.

        Mathf.RoundToInt(timer);

        if (timerText != null)
        {
            timerText.text = "TIME: " + timer.ToString();

            // Create a temporary reference to the current scene.
            Scene currentScene = SceneManager.GetActiveScene();

            // Retrieve the name of this scene.
            string sceneName = currentScene.name;

            if (sceneName == "Example 1")
            {
                // Do something...
            }
            else if (sceneName == "Example 2")
            {
                // Do something...
            }

            // Retrieve the index of the scene in the project's build settings.
            int buildIndex = currentScene.buildIndex;

            // Check the scene name as a conditional.
            switch (buildIndex)
            {
                case 0:
                    // Do something...
                    break;
                case 1:
                    // Do something...
                    break;
            }
        }
    }
}
