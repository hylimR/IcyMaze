using UnityEngine;
using System.Collections;

public class AlterPositionScript : MonoBehaviour {

    const string playerName = "unitychan";
    public GameObject block1, block2, block3, block4;

    void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == playerName)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                block1.GetComponent<AlterableBoxScript>().Move();
                block2.GetComponent<AlterableBoxScript>().Move();
                block3.GetComponent<AlterableBoxScript>().Move();
                block4.GetComponent<AlterableBoxScript>().Move();
            }
        }
    }
}
