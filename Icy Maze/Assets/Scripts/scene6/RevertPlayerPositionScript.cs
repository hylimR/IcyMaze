using IcyMaze;
using UnityEngine;

/// Sends the player back to the start of the Trial of Storms when a trap or a lightning
/// bolt catches them.
public class RevertPlayerPositionScript : MonoBehaviour
{
    const string TrapTag = "TrapBox";
    const string ThunderTag = "thunder";

    public int PlayerDie;

    Vector3 originalPos;
    Rigidbody body;

    void Start()
    {
        originalPos = transform.position;
        body = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TrapTag)) Respawn();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(ThunderTag)) Respawn();
    }

    void Respawn()
    {
        transform.position = originalPos;
        // Carrying the old velocity through a respawn used to fling the player straight
        // back into whatever had just killed them.
        if (body != null)
        {
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        PlayerDie++;
        GameProgress.RecordDeath();
    }
}
