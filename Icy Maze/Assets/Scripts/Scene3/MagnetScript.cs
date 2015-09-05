using UnityEngine;
using System.Collections;

public class MagnetScript : MonoBehaviour {

    private RaycastHit hit1, hit2, hit3, hit4;
    private Ray leftRay, rightRay, forwardRay, backRay;
	void Start () {
        leftRay = new Ray(transform.position, Vector3.left);
        rightRay = new Ray(transform.position, Vector3.right);
        forwardRay = new Ray(transform.position, Vector3.forward);
        backRay = new Ray(transform.position, Vector3.back);
    }

	void FixedUpdate () {
	    if(Physics.Raycast(leftRay, out hit1, 24f))
        {
            if (hit1.collider != null && hit1.collider.name == "unitychan")
            {
                hit1.collider.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000 * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
        if (Physics.Raycast(rightRay, out hit2, 24f))
        {
            if (hit2.collider != null && hit2.collider.name == "unitychan")
            {
                hit2.collider.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 1000 * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
        if (Physics.Raycast(forwardRay, out hit3, 24f))
        {
            if (hit3.collider != null && hit3.collider.name == "unitychan")
            {
                hit3.collider.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 1000 * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
        if (Physics.Raycast(backRay, out hit4, 24f))
        {
            if (hit4.collider != null && hit4.collider.name == "unitychan")
            {
                hit4.collider.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 1000 * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
    }
}
