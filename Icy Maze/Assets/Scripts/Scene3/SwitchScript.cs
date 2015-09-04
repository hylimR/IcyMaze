using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour
{
    const string playerName = "unitychan";
    public GameObject connectedBlock;
    private bool isActivated = false;

    void Start()
    {
        
    }

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

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.name == playerName)
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
