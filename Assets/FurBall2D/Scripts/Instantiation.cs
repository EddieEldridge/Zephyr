using UnityEngine;
using System.Collections;

public class Instantiation : MonoBehaviour {

	public GameObject gameObject = null;
	// Use this for initialization
	void Start () {

		gameObject = Instantiate(Resources.Load("Prefabs/Cloud")) as GameObject;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
