using UnityEngine;

/// Beam source. Fires along world forward -- the same axis the 2015 puzzle was laid out
/// against, so this deliberately ignores the emitter's own rotation.
[RequireComponent(typeof(LineRenderer))]
public class RayEmitterScript : MonoBehaviour
{
    [SerializeField] float rayDistance = 24f;
    [SerializeField] float beamSeconds = 3f;

    LineRenderer lineRenderer;
    float beamTimer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (beamTimer <= 0f) return;

        beamTimer -= Time.deltaTime;
        if (beamTimer <= 0f) lineRenderer.enabled = false;
    }

    public void EmitRay()
    {
        if (beamTimer > 0f) return;

        beamTimer = beamSeconds;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);

        if (!Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, rayDistance))
        {
            lineRenderer.SetPosition(1, transform.position + Vector3.forward * rayDistance);
            return;
        }

        // The original assumed the first thing hit was always a tube and threw a null
        // reference the moment anything else got in the way.
        ArrowTubeScript tube = hit.collider.GetComponent<ArrowTubeScript>();
        lineRenderer.SetPosition(1, tube != null ? tube.transform.position : hit.point);
        if (tube != null) tube.ChangeRayDirection();
    }
}
