using UnityEngine;
using System.Collections;

public class RayReceiverScript : MonoBehaviour {

    public GameObject gate;

    public void OpenGate()
    {
        gate.GetComponent<GateScript>().Open();
    }
}
