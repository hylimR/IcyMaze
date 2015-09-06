using UnityEngine;
using System.Collections;

public class FireBoxScript : MonoBehaviour {

	public GameObject unityChan;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag ==  "Playerchan") {
			other.transform.position = new Vector3 (0f, 0f, 0f);
		}

	}
}
