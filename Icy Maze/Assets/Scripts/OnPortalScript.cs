using UnityEngine;
using System.Collections;

public class OnPortalScript : MonoBehaviour {
	private GameObject LeftBottom;
	private GameObject LeftTop;
	private GameObject RightBottom;
	private GameObject RightTop;
	private TriggersheetScript LeftBottomScript;
	private TriggersheetScript LeftTopScript;
	private TriggersheetScript RightBottomScript;
	private TriggersheetScript RightTopScript;



	// Use this for initialization
	void Start () {
		LeftBottom = GameObject.Find ("LeftBottom");
		LeftTop = GameObject.Find ("LeftTop");
		RightBottom = GameObject.Find ("RightBottom");
		RightTop = GameObject.Find ("RightTop");

	
	}
	
	// Update is called once per frame
	void Update () {
		LeftBottomScript=LeftBottom.GetComponent<TriggersheetScript>();
		RightBottomScript=RightBottom.GetComponent<TriggersheetScript>();
		LeftTopScript=LeftTop.GetComponent<TriggersheetScript>();
		RightTopScript=RightTop.GetComponent<TriggersheetScript>();


	
	}
	void OnGUI(){
		if (LeftBottomScript.isOn && RightBottomScript.isOn && LeftTopScript.isOn && RightTopScript.isOn) {
			GUI.TextArea(new Rect(Screen.width/2,Screen.height/2,100,100),"You win");
		}
	}

}
