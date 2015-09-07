using UnityEngine;
using System.Collections;

public class WaterGunScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// The speed of water gun travel and its maximum range
	void Update () {
		transform.Translate (-0.5f,0f,0f);
		if (transform.position.x <= -30f) {
			transform.position = new Vector3(-29.9f,10f,600f);
		}
		
	}
	//When water gun hit the fire box, it will destroy the firebox
	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "fire") {
			transform.position = new Vector3(-29.9f,10f,600f);
			Destroy(other.gameObject);
		}
	}
}
