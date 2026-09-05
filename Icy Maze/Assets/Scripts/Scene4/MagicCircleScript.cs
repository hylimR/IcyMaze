using UnityEngine;

/// Lights up while at least one rune block is standing on it.
public class MagicCircleScript : MonoBehaviour
{
    const string BlockTag = "HitTube";

    int occupants;

    public bool isSteppedOn => occupants > 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(BlockTag)) occupants++;
    }

    // Counted rather than a plain bool: two blocks overlapping one circle used to clear
    // the flag as soon as either of them stepped off.
    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(BlockTag)) occupants = Mathf.Max(0, occupants - 1);
    }
}
