using UnityEngine;
using System.Collections;

public class HitTubeScript : MonoBehaviour
{
    public enum Direction { none, left, right, front, behind }
    private GameObject colliderObj;
    private Vector3 destination;
    private bool disableLeft, disableRight, disableFront, disableBack;
    private Direction moveDirection = Direction.none;
    private bool leftBounded, rightBounded, frontBounded, backBounded;

    void Start()
    {
        //Initialise destination to current position
        StopMovement();
    }

    // Update is called once per frame
    void Update()
    {
        //Perform checking every translation
        frontBounded = transform.localPosition.z < 12.005f ? true : false;
        backBounded = transform.localPosition.z > -0.005f ? true : false;
        leftBounded = transform.localPosition.x > -0.005f ? true : false;
        rightBounded = transform.localPosition.x < 12.005f ? true : false;

        if (!V3Equal(transform.localPosition,destination) && moveDirection != Direction.none)
        {
            if (moveDirection == Direction.left && leftBounded && !disableLeft)
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
            else if (moveDirection == Direction.right && rightBounded && !disableRight)
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            }
            else if (moveDirection == Direction.behind && backBounded && !disableBack)
            {
                transform.Translate(Vector3.back * Time.deltaTime);
            }
            else if (moveDirection == Direction.front && frontBounded && !disableFront)
            {
                transform.Translate(Vector3.forward * Time.deltaTime);
            }
            else
            {
                moveDirection = Direction.none;
                StopMovement();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        colliderObj = collision.collider.gameObject;
        if (colliderObj.name == "unitychan")
        {
            //get the collision point to check impact direction
            Direction direction = CalculateCollisionDirection(collision.contacts[0].point, transform);
            PerformMovement(direction);
        }

        if (colliderObj.GetComponent<HitTubeScript>() != null)
        {
            print("sss");
            colliderObj.GetComponent<HitTubeScript>().PerformMovement(moveDirection);
            StopMovement();
        }

    }

    public void PerformMovement(Direction direction)
    {
        float distanceToMove = 0;
        if (direction == Direction.left)
        {
            ChangeDirection(Direction.left);
            distanceToMove = transform.localPosition.x - 3.0f;
            destination = new Vector3(distanceToMove, transform.localPosition.y, transform.localPosition.z);
        }
        else if (direction == Direction.right)
        {
            ChangeDirection(Direction.right);
            distanceToMove = transform.localPosition.x + 3.0f;
            destination = new Vector3(distanceToMove, transform.localPosition.y, transform.localPosition.z);
        }

        else if (direction == Direction.behind)
        {
            ChangeDirection(Direction.behind);
            distanceToMove = transform.localPosition.z - 3.0f;
            destination = new Vector3(transform.localPosition.x, transform.localPosition.y, distanceToMove);

        }
        else
        {
            ChangeDirection(Direction.front);
            distanceToMove = transform.localPosition.z + 3.0f;
            destination = new Vector3(transform.localPosition.x, transform.localPosition.y, distanceToMove);
        }
    }

    public void StopMovement()
    {
        destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        ChangeDirection(Direction.none);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void ChangeDirection(Direction direction)
    {
        moveDirection = direction;
    }

    public void disableMovement(Direction direction)
    {
        switch (direction)
        {
            case Direction.left: disableLeft = true;
                break;
            case Direction.right: disableRight = true;
                break;
            case Direction.behind: disableBack = true;
                break;
            case Direction.front: disableFront = true;
                break;
            default: //Do nothing
                break;
        }
    }

    public void enableMovement(Direction direction)
    {
        switch (direction)
        {
            case Direction.left:
                disableLeft = false;
                break;
            case Direction.right:
                disableRight = false;
                break;
            case Direction.behind:
                disableBack = false;
                break;
            case Direction.front:
                disableFront = false;
                break;
            default: //Do nothing
                break;
        }
    }

    public static Direction CalculateCollisionDirection(Vector3 point, Transform transform)
    {
        Vector3 relativePosition = transform.InverseTransformPoint(point); //get the collision point to check impact direction
        if (relativePosition.x > 0 && relativePosition.z < relativePosition.x)
        {
            return Direction.left;
        }
        else if (relativePosition.x < 0 && relativePosition.z > relativePosition.x)
        {
            return Direction.right;
        }

        else if (relativePosition.z > 0 && relativePosition.x < relativePosition.z)
        {
            return Direction.behind;
        }
        else
        {
            return Direction.front;
        }
    }

    public static bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.0001;
    }
}
