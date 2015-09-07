using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour
{
    private Vector3 destination;
    void Start()
    {
        destination = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, 2f * Time.deltaTime);
    }

    //Move the gate to the predefined locatin when the method is called
    public void Open()
    {
        destination = transform.position + Vector3.left * 29.5f;
    }
}
