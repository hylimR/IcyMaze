using UnityEngine;
using System.Collections;

public class TriggersheetScript : MonoBehaviour {
	bool isOn;
	// Use this for initialization
	void Start () {
		isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision other){
		if (other.collider.CompareTag ("ElementBox")) {
			isOn = true;

		}
	
}
}
