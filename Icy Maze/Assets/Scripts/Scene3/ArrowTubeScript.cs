using IcyMaze;
using UnityEngine;

/// Rotatable mirror tube that relays the light beam. Walk into it and press the action
/// key to turn it 90 degrees.
[RequireComponent(typeof(LineRenderer))]
public class ArrowTubeScript : MonoBehaviour
{
    const string ArrowTubeTag = "ArrowTube";
    const string RayReceiverName = "RayReceiver";
    const int MaxRelays = 16;

    public Vector3 currentFacingDirection = Vector3.forward;

    [SerializeField] float rayCastDistance = 40f;
    [SerializeField] float beamSeconds = 3f;

    LineRenderer lineRenderer;
    ActionLatch action;
    float beamTimer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Start() => UpdateDirection();

    void Update()
    {
        if (beamTimer <= 0f) return;

        beamTimer -= Time.deltaTime;
        if (beamTimer <= 0f) lineRenderer.enabled = false;
    }

    void OnCollisionStay(Collision col)
    {
        if (!PlayerRef.Is(col.gameObject) || !action.Consume()) return;

        transform.Rotate(0f, 90f, 0f);
        UpdateDirection();
    }

    public void ChangeRayDirection() => Relay(MaxRelays);

    void Relay(int budget)
    {
        // Already lit tubes are skipped, which keeps a pair of tubes facing each other
        // from bouncing the beam forever; the relay budget is a second backstop.
        if (budget <= 0 || beamTimer > 0f) return;

        beamTimer = beamSeconds;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);

        if (!Physics.Raycast(transform.position, currentFacingDirection, out RaycastHit hit, rayCastDistance))
        {
            lineRenderer.SetPosition(1, transform.position + currentFacingDirection * rayCastDistance);
            return;
        }

        GameObject target = hit.collider.gameObject;
        ArrowTubeScript nextTube = target.CompareTag(ArrowTubeTag) ? target.GetComponent<ArrowTubeScript>() : null;
        lineRenderer.SetPosition(1, nextTube != null ? nextTube.transform.position : hit.point);
        if (nextTube != null) nextTube.Relay(budget - 1);

        RayReceiverScript receiver = target.GetComponent<RayReceiverScript>();
        if (receiver == null && target.name == RayReceiverName) receiver = target.GetComponentInParent<RayReceiverScript>();
        if (receiver != null) receiver.OpenGate();
    }

    /// Rounding eulerAngles.y straight to an int missed the 360 case, so a tube that had
    /// been turned all the way round kept firing in its previous direction.
    void UpdateDirection()
    {
        switch (Mathf.RoundToInt(transform.eulerAngles.y / 90f) & 3)
        {
            case 0: currentFacingDirection = Vector3.forward; break;
            case 1: currentFacingDirection = Vector3.right; break;
            case 2: currentFacingDirection = Vector3.back; break;
            default: currentFacingDirection = Vector3.left; break;
        }
    }
}
