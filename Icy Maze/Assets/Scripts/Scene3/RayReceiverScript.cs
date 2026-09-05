using UnityEngine;

public class RayReceiverScript : MonoBehaviour
{
    public GameObject gate;

    public void OpenGate()
    {
        GateScript target = gate != null ? gate.GetComponent<GateScript>() : null;
        if (target != null) target.Open();
    }
}
