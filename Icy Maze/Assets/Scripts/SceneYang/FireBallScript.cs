using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0.5f,0f,0f);
		if (transform.position.x >= 1.5f) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "Playerchan") {
			Destroy (this.gameObject);
		}
	}
}
