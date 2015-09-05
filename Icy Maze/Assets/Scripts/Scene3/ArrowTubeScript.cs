using UnityEngine;
using System.Collections;

public class ArrowTubeScript : MonoBehaviour
{

    public Vector3 currentFacingDirection;
    LineRenderer lineRenderer;
    private float rayCastDistance;
    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        rayCastDistance = 40f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name == "unitychan")
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                transform.Rotate(0, 90f, 0f);
                UpdateDirection();
            }
        }
    }

    public void ChangeRayDirection()
    {
        if (lineRenderer.enabled == false)
        {
            Ray ray = new Ray(transform.position, currentFacingDirection);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, rayCastDistance))
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
                if (hit.collider.gameObject.tag == "ArrowTube")
                {
                    lineRenderer.SetPosition(1, hit.collider.gameObject.transform.position);
                    hit.collider.gameObject.GetComponent<ArrowTubeScript>().ChangeRayDirection();
                }
                else
                {
                    lineRenderer.SetPosition(1, hit.point);
                }
            }
            Invoke("DisableRayRenderer", 3f);
        }
    }

    void DisableRayRenderer()
    {
        lineRenderer.enabled = false;
    }

    void UpdateDirection()
    {
        int y = (int)(Mathf.Round(transform.eulerAngles.y));
        switch (y)
        {
            case 0:
                currentFacingDirection = Vector3.forward;
                break;
            case 90:
                currentFacingDirection = Vector3.right;
                break;
            case 180:
                currentFacingDirection = Vector3.back;
                break;
            case 270:
                currentFacingDirection = Vector3.left;
                break;
        }
    }


}
