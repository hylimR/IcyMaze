using UnityEngine;
using System.Collections;

public class BoxMovingScript : MonoBehaviour
{
    private GameObject touchedBox;
    private UnityChanControlScriptWithRgidBody playerScript;
    void Start()
    {
        playerScript = GetComponent<UnityChanControlScriptWithRgidBody>();
    }

    void Update()
    {

    }

    void OnCollisionStay(Collision collision)
    {
        touchedBox = collision.collider.gameObject;
        if (touchedBox.tag == "MovableBox")
        {
            if (Input.GetKey(KeyCode.F))
            {
                touchedBox.transform.parent = transform;
                touchedBox.GetComponent<Rigidbody>().isKinematic = false;
                playerScript.rotateSpeed = 0f;
            }
            else
            {
                touchedBox.GetComponent<Rigidbody>().isKinematic = true;
                touchedBox.transform.parent = null;
                playerScript.rotateSpeed = 1.0f;
            }
        }
    }


    void OnCollisionExit(Collision collisionInfo)
    {
        if (touchedBox.tag == "MovableBox")
        {
            touchedBox.transform.parent = null;
            touchedBox.GetComponent<Rigidbody>().isKinematic = true;
            playerScript.rotateSpeed = 1.0f;
        }
    }
}
