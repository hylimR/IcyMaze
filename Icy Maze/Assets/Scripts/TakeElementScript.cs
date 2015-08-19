using UnityEngine;
using System.Collections;

public class TakeElementScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	
	}
	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "Playerchan") {
			if(Input.GetKey(KeyCode.F)){
			transform.parent = other.transform;
			other.rigidbody.isKinematic = true;
		} 
			else
			{
			transform.parent = null;
			other.rigidbody.isKinematic = false;
		}


	}

	}


}
