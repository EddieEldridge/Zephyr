using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RaycastController {

    // Vector to move our platform
    public Vector3 move;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 velocity = move * Time.deltaTime;
        transform.Translate(velocity);

    }
}
