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
        //if block is currently moving don't accept input, else move the object by set the destination approriately
        if (!isMoving())
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!isReverse)
                    destination = new Vector3(transform.localPosition.x - 1f, transform.localPosition.y, transform.localPosition.z);
                else
                    destination = new Vector3(transform.localPosition.x + 1f, transform.localPosition.y, transform.localPosition.z);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (!isReverse)
                    destination = new Vector3(transform.localPosition.x + 1f, transform.localPosition.y, transform.localPosition.z);
                else
                    destination = new Vector3(transform.localPosition.x - 1f, transform.localPosition.y, transform.localPosition.z);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if(!isReverse)
                    destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1f);
                else
                    destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 1f);
            }

            if (Input.GetKeyDown(KeyCode.S))
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
        //use fixed update to slowly update object position
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, moveSpeed * Time.deltaTime);
    }

    //Toggle box to either move in reverse direction/vice versa
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
    //Check box is currently moving
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
    //Reverse the direction on mouse click
    void OnMouseDown()
    {
        ReverseMovement();
    }
}
