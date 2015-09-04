using UnityEngine;
using System.Collections;

public class SlipperyGroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider col)
    {
        if (col.name == "unitychan")
        {
            col.GetComponent<PlayerMovementScript>().canMove = false;
            col.GetComponent<Rigidbody>().AddForce(col.transform.forward * 50, ForceMode.Acceleration);
            if(!col.GetComponent<PlayerMovementScript>().isMoving())
            {
                col.GetComponent<PlayerMovementScript>().canMove = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.name == "unitychan")
        {
            col.GetComponent<PlayerMovementScript>().canMove = true;
        }
    }
}
