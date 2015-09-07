using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public GameObject fireBall;
	public GameObject waterGun;
	public int bulletNo=3;
    private Vector3 oriPosition;
	// Use this for initialization
	void Start () {
        oriPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.J)) {
			if(bulletNo>0){
				waterGun = (GameObject)Instantiate (waterGun, new Vector3 (transform.position.x - 2f , transform.position.y, transform.position.z), Quaternion.identity);
				bulletNo--;
			}
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "fire") {
            transform.localPosition = oriPosition;
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
            Destroy(GameObject.Find(MasterScript.thirdScene));
            MasterScript.isThirdSceneCompleted = true;
            MasterScript.main.SetActive(true);
		}
	}
}
