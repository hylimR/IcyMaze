using UnityEngine;
using System.Collections;

public class TrapBoxScript : MonoBehaviour
{
    public float Speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Speed * Time.deltaTime, 0f, 0f);
    }
    void OnTriggerEnter(Collider other)//let trapbox revert direction
    {
        Speed = Speed * -1;
    }
}
