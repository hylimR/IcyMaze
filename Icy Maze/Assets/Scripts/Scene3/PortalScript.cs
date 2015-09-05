using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

    public string Scene;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "unitychan")
        {
            if (!MasterScript.isFirstSceneCompleted)
            {
                Application.LoadLevelAdditive(Scene);
            }
        }
    }
}
