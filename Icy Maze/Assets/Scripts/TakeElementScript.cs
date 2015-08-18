using UnityEngine;
using System.Collections;

public class TakeElementScript : MonoBehaviour {
	bool isTake;
	// Use this for initialization
	void Start () {
		isTake = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.F)) {
			isTake = true;
		}
		if(Input.GetKeyUp(KeyCode.F))
		{

				transform.parent=null;
			isTake=false;

		}
	
	}
	void OnCollisionEnter(Collision other){
		if(other.collider.CompareTag("Playerchan")){
			if(isTake){
				transform.parent = other.transform;}

		}

	}


}
