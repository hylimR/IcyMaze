using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public GameObject fireBall;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "fire") {
			transform.position = new Vector3 (3f, -5f, -3f);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.name.Equals ("Trap")) {
			GameObject fireBallSpawn = (GameObject)Instantiate (fireBall, new Vector3 (-33, -4.2f, -1.2f), Quaternion.identity);
			
		}
		if (other.name.Equals ("Trap2")) {
			GameObject fireBallSpawn = (GameObject)Instantiate (fireBall, new Vector3 (-33, -4.2f, -5.5f), Quaternion.identity);			
		}
		if (other.name.Equals ("Finish")) {
						
		}
	}
}
