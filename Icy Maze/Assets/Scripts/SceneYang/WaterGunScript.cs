using UnityEngine;
using System.Collections;

public class WaterGunScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (-0.5f,0f,0f);
		if (transform.position.x <= -30f) {
			transform.position = new Vector3(10f,10f,10f);
		}
	
	}
	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "fire") {
			transform.position = new Vector3(10f,10f,10f);
			Destroy(other.gameObject);
		}
	}
}
