using UnityEngine;

/// Patrolling hazard that reverses when it reaches a turn-around trigger.
public class TrapBoxScript : MonoBehaviour
{
    public float Speed = 5f;

    [SerializeField] float turnCooldown = 0.25f;

    float nextTurn;

    void Update() => transform.Translate(Speed * Time.deltaTime, 0f, 0f);

    // Overlapping turn triggers used to flip the direction twice in a row, leaving the
    // box stuck grinding against the wall it had just bounced off.
    void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextTurn) return;

        nextTurn = Time.time + turnCooldown;
        Speed = -Speed;
    }
}
