using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour
{

    public GameObject connectedTube;
    private LensFlare lenFlare;
    // Use this for initialization
    void Start()
    {
        lenFlare = connectedTube.GetComponentInChildren<LensFlare>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.gameObject.name);
        if (collision.collider.tag == "MovableBox")
        {
            lenFlare.brightness = 1.0f;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "MovableBox")
        {
            lenFlare.brightness = 0.1f;
        }
    }
}
