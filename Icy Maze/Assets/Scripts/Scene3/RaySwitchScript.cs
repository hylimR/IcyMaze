using UnityEngine;
using System.Collections;

public class RaySwitchScript : MonoBehaviour {

    public GameObject rayEmitter;

    //Activate the ray emitter
    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.name == MasterScript.playerName)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                rayEmitter.GetComponent<RayEmitterScript>().EmitRay();
            }
        }
    }
}
