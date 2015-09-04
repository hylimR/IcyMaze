using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour
{
    public GameObject player;       

    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 6f, 15f, player.transform.position.z);
    }
}
