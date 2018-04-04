using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    // Variables
    public Sprite flag1;
    public Sprite flag2;
    private SpriteRenderer checkpointSpriteRenderer;
    public bool checkpointReached;

	// Use this for initialization
	void Start ()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            checkpointSpriteRenderer.sprite = flag2;
            checkpointReached = true;
        }
    }
}
