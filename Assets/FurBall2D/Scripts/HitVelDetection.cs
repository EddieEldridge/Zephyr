using UnityEngine;
using System.Collections;


public class HitVelDetection : MonoBehaviour {
	
	// Use this for initialization
	void Start(){
		

	}

	void OnCollisionEnter2D(Collision2D collision2D) {

		if (collision2D.relativeVelocity.magnitude > 35)
			Destroy(gameObject);
		//Debug.Log ("Check");
	
	}



	

	
	// Update is called once per frame
	void Update () {
		
	}
}