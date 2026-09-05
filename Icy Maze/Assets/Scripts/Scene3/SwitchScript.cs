using IcyMaze;
using UnityEngine;

/// Toggles a blocking wall on and off.
public class SwitchScript : MonoBehaviour
{
    public GameObject connectedBlock;

    ActionLatch action;
    bool isActivated;

    void Start() => Apply();

    void OnTriggerStay(Collider col)
    {
        if (!PlayerRef.Is(col) || !action.Consume()) return;

        isActivated = !isActivated;
        Apply();
    }

    // The original called SetActive on the wall every single frame from Update.
    void Apply()
    {
        if (connectedBlock != null) connectedBlock.SetActive(!isActivated);
    }
}
