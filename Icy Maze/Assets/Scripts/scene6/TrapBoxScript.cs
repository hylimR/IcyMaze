using UnityEngine;
using System.Collections;

public class TrapBoxScript : MonoBehaviour {
	float x = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (x*Time.deltaTime, 0f, 0f);	
		
	}
	void OnTriggerEnter(Collider other){


			x = x * -1;

	}
}
