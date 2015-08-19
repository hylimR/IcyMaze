using UnityEngine;
using System.Collections;

public class TriggersheetScript : MonoBehaviour {
	public bool isOn;
	// Use this for initialization
	void Start () {
		isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision other){
		if (other.collider.tag=="ElementBox") {

			isOn = true;
			print("asd");

		}
	
}
}
