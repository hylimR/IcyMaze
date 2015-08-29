using UnityEngine;
using System.Collections;

public class MagicCircleScript : MonoBehaviour {

    public bool isSteppedOn = false;

	void Start () {
	    
	}
	
	void Update () {
	    
	}

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "HitTube")
        {
            isSteppedOn = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "HitTube")
        {
            isSteppedOn = false;
        }
    }
}
