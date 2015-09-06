using UnityEngine;
using System.Collections;

public class SlipperyGroundScript : MonoBehaviour {

    //When player in on current ground, force will be added to it whenever it try to make a movement
    void OnTriggerStay(Collider col)
    {
        if (col.name == MasterScript.playerName)
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
            col.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
