using UnityEngine;
using System.Collections;

public class AlterableBoxScript : MonoBehaviour {

    public Vector3 destination;
    private Vector3 originalPos;
    private float speed;
    private bool shouldMove;

    private Vector3 go, back;

	void Start () {
        speed = 1.5f;
        originalPos = transform.localPosition;  //Store the original location
        go = originalPos;
        back = originalPos;
        shouldMove = true;
	}

	void FixedUpdate () {
        transform.localPosition = Vector3.MoveTowards(back, go, speed * Time.fixedDeltaTime);
	}
    //Move the object to positions using toggled by other game object
    public void Move()
    {
        if (shouldMove)
        {
            go = destination;
            back = originalPos;
            shouldMove = false;
        }
        else
        {
            go = originalPos;
            back = destination;
            shouldMove = true;
        }
    }
}
