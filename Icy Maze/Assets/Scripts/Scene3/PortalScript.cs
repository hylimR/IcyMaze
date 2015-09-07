using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

    public string scene;
    public GameObject mainScene;
    void Update()
    {
        //After scene is completed, distinguish the light
        if (MasterScript.IsSceneComplete(scene))
        {
            GetComponent<LensFlare>().brightness = 0.1f;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == MasterScript.playerName)
        {
            //load scene if the scene haven't be solved yet
            if (!MasterScript.IsSceneComplete(scene))
            {
                mainScene.SetActive(false);
                Application.LoadLevelAdditive(scene);
            }
        }
    }
}
