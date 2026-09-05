using IcyMaze;
using UnityEngine;

/// Ice. You keep sliding the way you were facing until you come to a stop.
public class SlipperyGroundScript : MonoBehaviour
{
    [SerializeField] float slideAcceleration = 50f;

    Collider cachedCollider;
    PlayerMovementScript cachedPlayer;
    Rigidbody cachedBody;

    void OnTriggerStay(Collider col)
    {
        if (!PlayerRef.Is(col) || !Resolve(col)) return;

        cachedBody.AddForce(col.transform.forward * slideAcceleration, ForceMode.Acceleration);
        // isMoving() used to advance its own reference position on every call, so asking
        // it here right after the player had already asked it returned false and control
        // was handed straight back -- the ice did nothing at all.
        cachedPlayer.canMove = !cachedPlayer.isMoving();
    }

    void OnTriggerExit(Collider col)
    {
        if (!PlayerRef.Is(col) || !Resolve(col)) return;

        cachedPlayer.canMove = true;
        cachedBody.linearVelocity = Vector3.zero;
    }

    // OnTriggerStay runs every physics step; the lookups are done once per collider.
    bool Resolve(Collider col)
    {
        if (col != cachedCollider)
        {
            cachedCollider = col;
            cachedPlayer = col.GetComponent<PlayerMovementScript>();
            cachedBody = col.attachedRigidbody;
        }
        return cachedPlayer != null && cachedBody != null;
    }
}
