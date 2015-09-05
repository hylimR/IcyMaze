using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

    public string Scene;
    
    //load scene if the scene haven't be solved yet
    void OnTriggerEnter(Collider col)
    {
        if (col.name == MasterScript.playerName)
        {
            if (!MasterScript.isFirstSceneCompleted)
            {
                Application.LoadLevelAdditive(Scene);
            }
        }
    }
}
