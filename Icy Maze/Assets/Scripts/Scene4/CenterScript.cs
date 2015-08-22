using UnityEngine;
using System.Collections;

public class CenterScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "HitTube")
        {
            collision.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
