using IcyMaze;
using UnityEngine;

/// Rune block. Every block in the trial answers the same key press; the ones flagged
/// as reversed travel the opposite way, which is the whole puzzle.
public class HitTubeScriptV2 : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool isReverse = false;

    Vector3 destination;
    Behaviour halo;

    void Start()
    {
        destination = transform.localPosition;
        halo = GetComponent("Halo") as Behaviour;
        if (halo != null) halo.enabled = isReverse;
    }

    void Update()
    {
        if (isMoving()) return;

        Vector2 nudge = GameInput.NudgePressed;
        if (nudge == Vector2.zero) return;

        Vector3 step = new Vector3(nudge.x, 0f, nudge.y);
        destination = transform.localPosition + (isReverse ? -step : step);
    }

    void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, moveSpeed * Time.fixedDeltaTime);
    }

    public void ReverseMovement()
    {
        isReverse = !isReverse;
        if (halo != null) halo.enabled = isReverse;
    }

    /// Compares against the target tile rather than against last frame's position, which
    /// reported "stopped" on the first frame of a slide and let a second nudge through.
    public bool isMoving() => (transform.localPosition - destination).sqrMagnitude > 1e-6f;

    void OnMouseDown() => ReverseMovement();
}
