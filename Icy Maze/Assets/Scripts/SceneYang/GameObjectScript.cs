using UnityEngine;
using System.Collections;

public class GameObjectScript : MonoBehaviour {
	public GameObject fireBox;
	float timeElapsed = 0.5f;
	//The x and z coordinates of the initial firebox
	float[] X = {-3f,-5f,-8f,-10f,-12f, -14f,-14f, -17f,-20f, -23f, -27f,-27f};
	float[] Z = {-6.8f, -3.3f, 0.5f,-6.8f,0.5f,-3.3f,-6.8f, 0.5f,-3.3f, -6.8f , 0.5f ,-6.8f};
	//The x and z coordinates of extra fire box.
	float[] XDrop = {-2.7f,-8f,-8f,-17f,-20f,-23f,-27f};
	float[] ZDrop = {-3.3f,-3.3f,-6.8f,-6.8f,0.5f,0.5f,-3.3f};int loop = 0;
	bool check = true;

	// Use this for initialization
	void Start () {
		//This will create the initial fire box first when the game starts.
		for (int i=0; i<12; i++) {
			GameObject appear = (GameObject)Instantiate (fireBox, new Vector3 (X[i], -3f, Z[i]), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {

		//After the game start, this code will generate extra fire box that will drop from top every 1 second.
		if (check) {
			timeElapsed += Time.deltaTime;
			if (timeElapsed >= 1f) {
				GameObject appear = (GameObject)Instantiate (fireBox, new Vector3 (XDrop[loop], 10f, ZDrop[loop]), Quaternion.identity);
				timeElapsed = 0f;
				loop++;
			}
			if(loop>7){
				check = false;
			}
		} else {
		}
	}

	void OnGUI()
	{
		{
			//instruction
			GUI.Box(new Rect(Screen.width/2 + 350, Screen.height/2 - 300, 250, 90),
			        "Use 'ASDW' keys to move.\nPress J to shoot water gun (3bullets).\nGoal is to reach the end.\nAvoid flame box and flame ball.\nBeware of trap");		
		}
	}
}
