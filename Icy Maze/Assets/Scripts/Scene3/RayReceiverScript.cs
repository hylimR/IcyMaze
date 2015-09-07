using UnityEngine;
using System.Collections;

public class RayReceiverScript : MonoBehaviour {

    public GameObject gate;
    //Open the gate
    public void OpenGate()
    {
        gate.GetComponent<GateScript>().Open();
    }
}
