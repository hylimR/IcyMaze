using UnityEngine;
using System.Collections;

public class AlterPositionScript : MonoBehaviour {
    public GameObject block1, block2, block3, block4;

    //toggle the position of the four block
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == MasterScript.playerName)
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
