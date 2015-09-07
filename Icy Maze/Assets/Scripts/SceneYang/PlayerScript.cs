using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public GameObject fireBall;
	public GameObject waterGun;
	public int bulletNo=3; //The maximum number of water gun
    private Vector3 oriPosition;
	// Use this for initialization
	void Start () {
        oriPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		//pressing J key will creates water gun infront of player
		if (Input.GetKeyUp (KeyCode.J)) {
			if(bulletNo>0){
				waterGun = (GameObject)Instantiate (waterGun, new Vector3 (transform.position.x - 2f , transform.position.y, transform.position.z), Quaternion.identity);
				bulletNo--;
			}
		}
	}
	//When fire ball or firebox hit player, it will go back to initial position
	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "fire") {
            transform.localPosition = oriPosition;
		}
	}

	void OnTriggerEnter(Collider other){
		//When player steps on the trap, fire ball will be spawn
		if (other.name.Equals ("Trap")) {
			GameObject fireBallSpawn = (GameObject)Instantiate (fireBall, new Vector3 (-33, -4.2f, -1.2f), Quaternion.identity);		
		}
		//When player steps on the trap, fire ball will be spawn
		if (other.name.Equals ("Trap2")) {
			GameObject fireBallSpawn = (GameObject)Instantiate (fireBall, new Vector3 (-33, -4.2f, -5.0f), Quaternion.identity);			
		}
		//When player steps on the finish line, this scene will end
		if (other.name.Equals ("Finish")) {
            Destroy(GameObject.Find(MasterScript.thirdScene));
            MasterScript.isThirdSceneCompleted = true;
            MasterScript.main.SetActive(true);
		}
	}
}
