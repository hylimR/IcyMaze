using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

    public string scene;
    public GameObject mainScene;
    //load scene if the scene haven't be solved yet
    void OnTriggerEnter(Collider col)
    {
        if (col.name == MasterScript.playerName)
        {
            if (!MasterScript.IsSceneComplete(scene))
            {
                mainScene.SetActive(false);
                Application.LoadLevelAdditive(scene);
            }
        }
    }
}
