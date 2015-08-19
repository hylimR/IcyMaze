using UnityEngine;
using System.Collections;

public class OnPortalScript : MonoBehaviour {
	private GameObject LeftBottom;
	private GameObject LeftTop;
	private GameObject RightBottom;
	private GameObject RightTop;
	private GameObject Portal;
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
		Portal = GameObject.Find ("Portal");
		Portal.SetActive(false);

	
	}
	
	// Update is called once per frame
	void Update () {
		LeftBottomScript=LeftBottom.GetComponent<TriggersheetScript>();
		RightBottomScript=RightBottom.GetComponent<TriggersheetScript>();
		LeftTopScript=LeftTop.GetComponent<TriggersheetScript>();
		RightTopScript=RightTop.GetComponent<TriggersheetScript>();
		if (LeftBottomScript.isOn && RightBottomScript.isOn && LeftTopScript.isOn && RightTopScript.isOn) {
			Portal.SetActive(true);
		}

	
	}


}
