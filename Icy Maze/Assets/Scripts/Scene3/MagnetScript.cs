using IcyMaze;
using UnityEngine;

/// Pulls the player in whenever they cross one of its four sight lines.
public class MagnetScript : MonoBehaviour
{
    static readonly Vector3[] Directions = { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    [SerializeField] float range = 24f;
    [SerializeField] float pullAcceleration = 20f;

    void FixedUpdate()
    {
        // Rays were built once in Start from the position the magnet had at load time.
        foreach (Vector3 direction in Directions)
        {
            if (!Physics.Raycast(transform.position, direction, out RaycastHit hit, range)) continue;
            if (!PlayerRef.Is(hit.collider)) continue;

            Rigidbody body = hit.rigidbody;
            if (body != null) body.AddForce(-direction * pullAcceleration, ForceMode.Acceleration);
        }
    }
}
