using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

    private int keys = 3;

	void Start () {
	
	}
	
	void Update () {
	    
	}

    void OnMouseDown()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(60f, 0f, 60f), ForceMode.Impulse);
    }
}
