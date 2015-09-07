using UnityEngine;
using System.Collections;

public class ThunderScript : MonoBehaviour {
	public GameObject thunder;
	public GameObject target;
	public int numberOfThunder;//minimum 8
	private GameObject[] thunderTobeDestroy;
	private GameObject[] targetTobeDestroy;
    private Vector3 parent;
	private Vector3[] targetPosition;

	void Start () {
		Invoke ("DisplayTarget", 0.5f);
        parent = transform.position;
		targetPosition = new Vector3[numberOfThunder];
	}

	void DisplayThunder(){
		for (int i =0; i<targetPosition.Length; i++) {	//generate thunder around the trigger sheet
			Instantiate (thunder,new Vector3(targetPosition[i].x,targetPosition[i].y+5.7f,targetPosition[i].z), Quaternion.identity);
		
		}
		Invoke ("DestroyThunder", 1f);
	}
	void DestroyThunder()
	{ // destroy the thunder
		thunderTobeDestroy = GameObject.FindGameObjectsWithTag ("thunder");
		for (int i=0; i<thunderTobeDestroy.Length; i++) {
			Destroy (thunderTobeDestroy[i]);
		}
		Invoke ("DisplayTarget", 0.5f);

	}
	void DisplayTarget(){
		for (float i=0; i<2; i++) 
		{	//generate target around the trigger sheet
			Instantiate (target, new Vector3 (parent.x + Random.Range(-11f,-7.5f), parent.y+0.3f , parent.z + i - 12f),Quaternion.identity);
			
			Instantiate (target, new Vector3 (parent.x  + Random.Range(11f,7.5f), parent.y+0.3f , parent.z + i - 12f),Quaternion.identity);
			
			Instantiate (target, new Vector3 (parent.x + Random.Range(11f,-7.5f), parent.y+0.3f, parent.z + i +8f),Quaternion.identity);
			
			Instantiate (target, new Vector3 (parent.x + Random.Range(11f,-7.5f), parent.y+0.3f , parent.z + i +8f),Quaternion.identity);
		}
		for (int i =0 ;i<numberOfThunder-8;i++)
		{//generate random target in the scene;
			Instantiate (target, new Vector3 (parent.x + Random.Range(7.5f,-7.5f), parent.y+0.3f , parent.z + Random.Range(13f,-7f)),Quaternion.identity);
		}
		Invoke ("DestroyTarget", 1f);
	}
	void DestroyTarget()
	{ // destory the target
		targetTobeDestroy = GameObject.FindGameObjectsWithTag ("target");
		for (int i=0; i<targetTobeDestroy.Length; i++) {
			targetPosition[i]=targetTobeDestroy[i].transform.position;
			Destroy (targetTobeDestroy[i]);
		}
		Invoke ("DisplayThunder", 0.5f);
		
	}
}
