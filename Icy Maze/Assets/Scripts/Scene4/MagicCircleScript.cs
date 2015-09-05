using UnityEngine;
using System.Collections;

public class MagicCircleScript : MonoBehaviour {

    //Check whether this particular magic circle is stepped on by box
    public bool isSteppedOn = false;

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
