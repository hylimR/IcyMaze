using UnityEngine;
using System.Collections;

public class BlockerScript : MonoBehaviour {

    public Vector3 pos1, pos2, pos3, pos4;
	IEnumerator Start () {
        while (true)
        {
            yield return StartCoroutine(Move(pos1));
            yield return StartCoroutine(Move(pos2));
            yield return StartCoroutine(Move(pos3));
            yield return StartCoroutine(Move(pos4));
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, Time.deltaTime * 180, Space.Self);
    }

    IEnumerator Move(Vector3 des)
    {
        float i = 0f;
        while(i < 1.0f)
        {
            i += Time.fixedDeltaTime;
            transform.localPosition = Vector3.Slerp(transform.localPosition, des, i * 4);
            yield return null;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(gameObject.name == "unitychan")
            col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-50,50), 0f, Random.Range(-50,-50)), ForceMode.Impulse);
    }
}
