using UnityEngine;
using System.Collections;

public class HitTubeScriptV2 : MonoBehaviour
{
    public float moveSpeed;
    public bool isReverse = false;

    private Vector3 lasPosition, curPosition;
    private Vector3 destination;
    Behaviour halo;

    void Start()
    {
        destination = transform.localPosition;
        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
    }

    void Update()
    {
        if (!isMoving())
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (!isReverse)
                    destination = new Vector3(transform.localPosition.x - 1f, transform.localPosition.y, transform.localPosition.z);
                else
                    destination = new Vector3(transform.localPosition.x + 1f, transform.localPosition.y, transform.localPosition.z);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (!isReverse)
                    destination = new Vector3(transform.localPosition.x + 1f, transform.localPosition.y, transform.localPosition.z);
                else
                    destination = new Vector3(transform.localPosition.x - 1f, transform.localPosition.y, transform.localPosition.z);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if(!isReverse)
                    destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1f);
                else
                    destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 1f);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                if(!isReverse)
                    destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 1f);
                else
                    destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1f);
            }
        }
    }

    void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, moveSpeed * Time.deltaTime);
    }

    public void ReverseMovement()
    {
        if (isReverse)
        {
            isReverse = false;
            halo.enabled = false;
        }
        else
        {
            isReverse = true;
            halo.enabled = true;
        }
    }

    public bool isMoving()
    {
        curPosition = transform.localPosition;
        if (curPosition == lasPosition)
        {
            return false;
        }
        lasPosition = curPosition;
        return true;
    }

    public static bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.1f;
    }

    void OnMouseDown()
    {
        ReverseMovement();
    }
}
