using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour
{
    public GameObject connectedBlock;
    private bool isActivated = false;

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            connectedBlock.SetActive(false);
        }
        else
        {
            connectedBlock.SetActive(true);
        }
    }

    //Toggle to open/close the blockage
    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.name == MasterScript.playerName)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!isActivated)
                    isActivated = true;
                else
                    isActivated = false;
            }
        }
    }
}
