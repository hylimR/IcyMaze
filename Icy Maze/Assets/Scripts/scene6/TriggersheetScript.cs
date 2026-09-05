using IcyMaze;
using UnityEngine;

/// Pressure plate that stays lit once the player has touched it.
public class TriggersheetScript : MonoBehaviour
{
    public bool isOn;

    Behaviour halo;

    void Start()
    {
        halo = GetComponent("Halo") as Behaviour;
        if (halo != null) halo.enabled = isOn;
    }

    void OnCollisionEnter(Collision col)
    {
        if (isOn || !PlayerRef.Is(col.gameObject)) return;

        isOn = true;
        if (halo != null) halo.enabled = true;
    }
}
