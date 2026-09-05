using UnityEngine;

/// Sliding gate opened by the light-beam receiver.
public class GateScript : MonoBehaviour
{
    [SerializeField] float openDistance = 29.5f;
    [SerializeField] float openSpeed = 2f;

    Vector3 destination;
    bool isOpen;

    void Start() => destination = transform.position;

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, openSpeed * Time.fixedDeltaTime);
    }

    /// Latched: the original recomputed the target from the gate's live position, so a
    /// second beam hit sent the gate another 29.5 units into the level geometry.
    public void Open()
    {
        if (isOpen) return;
        isOpen = true;
        destination = transform.position + Vector3.left * openDistance;
    }
}
