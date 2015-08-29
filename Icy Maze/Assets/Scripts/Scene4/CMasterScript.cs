using UnityEngine;
using System.Collections.Generic;

public class CMasterScript : MonoBehaviour {

    public GameObject mCircle1, mCircle2, mCircle3, mCircle4;
    MagicCircleScript s1, s2, s3, s4;
	void Start () {
        s1 = mCircle1.GetComponent<MagicCircleScript>();
        s2 = mCircle2.GetComponent<MagicCircleScript>();
        s3 = mCircle4.GetComponent<MagicCircleScript>();
        s4 = mCircle4.GetComponent<MagicCircleScript>();
    }
	
	void Update () {

    }

    void OnGUI()
    {
        GUIContent content = new GUIContent();
        content.text = "Completed Puzzle";
        GUIStyle style = new GUIStyle();
        style.fontSize = 40;
        GUI.Label(new Rect(500, 500, Screen.height, Screen.width), content, style);
        
    }

    private bool isPuzzleComplete()
    {
        if (s1.isSteppedOn && s2.isSteppedOn && s3.isSteppedOn && s4.isSteppedOn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
