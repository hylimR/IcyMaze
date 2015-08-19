using UnityEngine;
using System.Collections;

public class RevertPlayerPositionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(){
		transform.position = new Vector3 (0f,0f,-11f);

	}
}
