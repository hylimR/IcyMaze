using UnityEngine;
using System.Collections;

public class RayEmitterScript : MonoBehaviour {

    LineRenderer lineRenderer;
    float rayDistance;
	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        rayDistance = 24f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EmitRay()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.collider.gameObject.transform.position);
            hit.collider.gameObject.GetComponent<ArrowTubeScript>().ChangeRayDirection();
        }
        Invoke("DisableLightRenderer", 3f);
    }

    void DisableLightRenderer()
    {
        lineRenderer.enabled = false;
    }
}
