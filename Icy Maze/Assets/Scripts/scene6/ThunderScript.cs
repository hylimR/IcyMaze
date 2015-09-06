using UnityEngine;
using System.Collections;

public class ThunderScript : MonoBehaviour {
	public GameObject thunder;
	private GameObject[] thunderTobeDestory;
    private Vector3 parent;

	void Start () {
		Invoke ("DisplayThunder", 0.5f);
        parent = transform.position;
	}

	void DisplayThunder(){
		for (float i=0; i<5; i++) {
			Instantiate (thunder, new Vector3 (parent.x + Random.Range(-11f,-7.5f), parent.y + 6f, parent.z + i - 12f),Quaternion.identity);
		
			Instantiate (thunder, new Vector3 (parent.x  + Random.Range(11f,7.5f), parent.y + 6f, parent.z + i - 12f),Quaternion.identity);
		
			Instantiate (thunder, new Vector3 (parent.x + Random.Range(11f,-7.5f), parent.y + 6f, parent.z + i +8f),Quaternion.identity);
		
			Instantiate (thunder, new Vector3 (parent.x + Random.Range(11f,-7.5f), parent.y + 6f, parent.z + i +8f),Quaternion.identity);
		}
		for (int i =0 ;i<5;i++){
			Instantiate (thunder, new Vector3 (parent.x + Random.Range(7.5f,-7.5f), parent.y + 6f, parent.z + Random.Range(13f,-7f)),Quaternion.identity);
		}
		Invoke ("DestoryThunder", 1f);
	}
	void DestoryThunder(){
		thunderTobeDestory = GameObject.FindGameObjectsWithTag ("thunder");
		for (int i=0; i<thunderTobeDestory.Length; i++) {
			Destroy (thunderTobeDestory[i]);
		}
		Invoke ("DisplayThunder", 0.5f);

	}
}
