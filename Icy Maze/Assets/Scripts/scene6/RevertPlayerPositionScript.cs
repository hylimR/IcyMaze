using UnityEngine;
using System.Collections;

public class RevertPlayerPositionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "TrapBox") {
			transform.position = new Vector3 (0f, 0f, -10f);
		}
	}
}
