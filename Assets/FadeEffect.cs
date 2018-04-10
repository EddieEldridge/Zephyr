using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour {

    // Variables
    public Texture2D fadeOuteTexture; // the texture that will overlay
    public float fadeSpeed = 0.3f; // speed of the fade effect
    private int drawDepth = -1000; // textures draw order
    private float alpha = 1.0f; // texture's alpha values (0,1)
    private int fadeDir = -1; // direction to fade, -1 = fade in, 1 = fade out

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        alpha = Mathf.Clamp01(alpha);

        // Set colors
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOuteTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    private void OnLevelWasLoaded()
    {
        // When the level is loaded, fade in the new level from a black screen
        BeginFade(-1);
    }
}
