using UnityEngine;
using System.Collections;

public class RayEmitterScript : MonoBehaviour {

    LineRenderer lineRenderer;
    float rayDistance;

	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        rayDistance = 24f;
	}

    //Perform a raycast and instantiate a laser to the point of interception
    public void EmitRay()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit = new RaycastHit();
        //Trigger the method on arrow tube to transmit the ray to other direction
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.collider.gameObject.transform.position);
            hit.collider.gameObject.GetComponent<ArrowTubeScript>().ChangeRayDirection();
        }
        Invoke("DisableLightRenderer", 3f);
    }
    //Remove the laser
    void DisableLightRenderer()
    {
        lineRenderer.enabled = false;
    }
}
