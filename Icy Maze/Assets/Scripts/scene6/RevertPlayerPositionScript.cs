using UnityEngine;
using System.Collections;

public class RevertPlayerPositionScript : MonoBehaviour {
	public int PlayerDie;
    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

	void OnTriggerEnter(Collider other){
		if (other.tag == "TrapBox") {
			transform.position = originalPos;
			PlayerDie++;
		}
	}
	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "thunder") {
			transform.position = originalPos;
			PlayerDie++;
		}
	}
	void OnGUI(){
		GUI.Box(new Rect(0, 10, 250, 150), "Infomation");
		GUI.Label(new Rect(0, 30, 250, 30), "Winning Condition");
		GUI.Label(new Rect(0, 50, 250, 30), "Light up all the trigger sheet");
		GUI.Label(new Rect(0, 70, 250, 30), "Player Died :"+PlayerDie);
	}
}
