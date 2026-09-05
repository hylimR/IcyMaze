using UnityEngine;

/// Block that shuttles between its start position and a target.
public class AlterableBoxScript : MonoBehaviour
{
    public Vector3 destination;

    [SerializeField] float speed = 1.5f;

    Vector3 originalPos;
    bool atDestination;

    void Start() => originalPos = transform.localPosition;

    void FixedUpdate()
    {
        // The original interpolated from a fixed start vector rather than from where the
        // block actually was, so it snapped one step off its origin and stayed there.
        Vector3 target = atDestination ? destination : originalPos;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed * Time.fixedDeltaTime);
    }

    public void Move() => atDestination = !atDestination;
}
