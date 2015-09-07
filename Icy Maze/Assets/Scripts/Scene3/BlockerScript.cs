using UnityEngine;
using System.Collections;

public class BlockerScript : MonoBehaviour
{
    public Vector3 pos1, pos2, pos3, pos4;
    public float rate;
    //linearly repeat moving between the four position set
    void Start()
    {
        Physics.IgnoreLayerCollision(8, 8, true);
        StartCoroutine(Moving());
    }
    //Restart the coroutine when the game object is reactivated
    void OnEnable()
    {
        StartCoroutine(Moving());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * 180, Space.Self);
    }

    //Move the objects in iteration
    IEnumerator Move(Vector3 des)
    {
        float i = 0f;
        while (i < 1.0f)
        {
            i += Time.fixedDeltaTime;
            transform.localPosition = Vector3.Slerp(transform.localPosition, des, i * rate);
            yield return null;
        }
    }
    
    //Create a infinite loop to continuously move game object over four different pre-defined position
    IEnumerator Moving()
    {
        while (true)
        {
            yield return StartCoroutine(Move(pos1));
            yield return StartCoroutine(Move(pos2));
            yield return StartCoroutine(Move(pos3));
            yield return StartCoroutine(Move(pos4));
        }
    }

    //Apply a strong impulsive force to player whenever collided
    void OnCollisionStay(Collision col)
    {
        if (gameObject.name == MasterScript.playerName)
            col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-30, 30), 0f, Random.Range(-30, 30)), ForceMode.Impulse);
    }
}
