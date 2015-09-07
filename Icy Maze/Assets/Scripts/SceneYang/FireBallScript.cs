using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// The speed of fireball travel and its maximum range
	void Update () {
		transform.Translate (0.5f,0f,0f);
		if (transform.position.x >= 1.5f) {
			Destroy (this.gameObject);
		}
	}
	//when fireball hits the player, destroy fireball
	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "Playerchan") {
			Destroy (this.gameObject);
		}
	}
}
