using UnityEngine;
using System.Collections;

public class RaySwitchScript : MonoBehaviour {

    public GameObject rayEmitter;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.name == "unitychan")
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                rayEmitter.GetComponent<RayEmitterScript>().EmitRay();
            }
        }
    }
}
