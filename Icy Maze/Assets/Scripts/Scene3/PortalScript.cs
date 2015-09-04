using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        if (!MasterScript.isFirstSceneCompleted)
        {
            Application.LoadLevelAdditive(MasterScript.firstScene);
        }
    }
}
