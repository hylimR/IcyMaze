using UnityEngine;
using System;
using System.Collections;

public class TubeColliderScript : MonoBehaviour {

    public GameObject player;
    private Transform parent;
    private HitTubeScript.Direction direction;
    // Use this for initialization
    void Start() {
        parent = GetComponentInParent<Transform>();
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), true);
        Physics.IgnoreCollision(GetComponentInParent<Collider>(), GetComponent<Collider>(), true);
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "TubeCollider")
        {
            direction = HitTubeScript.CalculateCollisionDirection(collision.contacts[0].point, transform);
            GetComponentInParent<HitTubeScript>().StopMovement();
            GetComponentInParent<HitTubeScript>().disableMovement(direction);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        GetComponentInParent<HitTubeScript>().enableMovement(direction);
    }
}
