using IcyMaze;
using UnityEngine;

public class RaySwitchScript : MonoBehaviour
{
    public GameObject rayEmitter;

    RayEmitterScript emitter;
    ActionLatch action;

    void Start() => emitter = rayEmitter != null ? rayEmitter.GetComponent<RayEmitterScript>() : null;

    void OnTriggerStay(Collider col)
    {
        if (emitter != null && PlayerRef.Is(col) && action.Consume()) emitter.EmitRay();
    }
}
