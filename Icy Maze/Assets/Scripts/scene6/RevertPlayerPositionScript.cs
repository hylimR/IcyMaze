using UnityEngine;
using System.Collections;

public class RevertPlayerPositionScript : MonoBehaviour {
	public int PlayerDie;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "TrapBox") {
			transform.position = new Vector3 (0f, 0f, -10f);
			PlayerDie++;
			print (PlayerDie);
		}

	}
	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "thunder") {
			transform.position = new Vector3 (0f, 0f, -10f);
			PlayerDie++;
			print (PlayerDie);
		}
	}
}
