using UnityEngine;
using System.Collections;

public class BoxMovingScript : MonoBehaviour
{
    private GameObject touchedBox;
    void Start()
    {

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
            }
            else
            {
                touchedBox.transform.parent = null;
            }
        }
    }


    void OnCollisionExit(Collision collisionInfo)
    {
        touchedBox.transform.parent = null;
    }

}
