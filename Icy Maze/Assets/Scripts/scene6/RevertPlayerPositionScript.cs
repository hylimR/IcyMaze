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

		}

	}
	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "thunder") {
			transform.position = new Vector3 (0f, 0f, -10f);
			PlayerDie++;
			print (PlayerDie);
		}
	}
	void OnGUI(){
		GUI.Box(new Rect(0, 10, 250, 150), "Infomation");
		GUI.Label(new Rect(0, 30, 250, 30), "Winning Condition");
		GUI.Label(new Rect(0, 50, 250, 30), "Light up all the trigger sheet");
		GUI.Label(new Rect(0, 70, 250, 30), "Player Died :"+PlayerDie);

	}
}
