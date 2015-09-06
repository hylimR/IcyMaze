using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

    public string scene;
    //load scene if the scene haven't be solved yet
    void OnTriggerEnter(Collider col)
    {
        if (col.name == MasterScript.playerName)
        {
            if (!MasterScript.IsSceneComplete(scene))
            {
                col.gameObject.GetComponent<PlayerMovementScript>().canMove = false;
                Application.LoadLevelAdditive(scene);
            }
        }
    }
}
