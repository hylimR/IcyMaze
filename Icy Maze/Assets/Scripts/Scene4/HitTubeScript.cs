using UnityEngine;
using System.Collections;

public class HitTubeScript : MonoBehaviour
{
    enum Direction { none, left, right, front, behind }
    private GameObject colliderObj;
    private Vector3 destination;
    private Direction moveDirection = Direction.none;

    void Start()
    {
        //Initialise destination to current position
        destination = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition != destination && transform.position != destination)
        {
            if (moveDirection == Direction.left)
                transform.Translate(-0.2f, 0f, 0f);
            else if (moveDirection == Direction.right)
                transform.Translate(0.2f, 0f, 0f);
            else if (moveDirection == Direction.front)
                transform.Translate(0f, 0f, -0.2f);
            else if (moveDirection == Direction.behind)
                transform.Translate(0f, 0f, 0.2f);
            else
                moveDirection = Direction.none;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        colliderObj = collision.collider.gameObject;
        if (colliderObj.name == "unitychan")
        {
            Vector3 relativePosition = transform.InverseTransformPoint(collision.contacts[0].point);

            if (relativePosition.x > 0 && relativePosition.z < relativePosition.x)
            {
                moveDirection = Direction.left;
                destination = new Vector3(transform.localPosition.x - 3f, transform.localPosition.y, transform.localPosition.z);
            }
            else if (relativePosition.x < 0 && relativePosition.z > relativePosition.x)
            {
                moveDirection = Direction.right;
                destination = new Vector3(transform.localPosition.x + 3f, transform.localPosition.y, transform.localPosition.z);
            }

            else if (relativePosition.z > 0 && relativePosition.x < relativePosition.z)
            {
                moveDirection = Direction.front;
                destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 3f);
            }
            else
            {
                moveDirection = Direction.behind;
                destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 3f);
            }
        }

    }
}
