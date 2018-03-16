using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Menu : MonoBehaviour {

	// Function for navigating our main menu
    public void PlayGame ()
    {
        // Increment our scene forward 1 scene from the scene we are on 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Function for quitting the game
    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
