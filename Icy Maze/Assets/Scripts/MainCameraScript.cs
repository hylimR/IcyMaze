using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour
{
    //Lock camera on player
    public GameObject player;
	public float height;
	public float x_axis;
	public float z_axis;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x+ x_axis, 20f+height, player.transform.position.z+z_axis);
    }
}
