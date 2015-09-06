using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour
{
    //Lock camera on player
    public GameObject player;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 6f, 20f, player.transform.position.z);
    }
}
