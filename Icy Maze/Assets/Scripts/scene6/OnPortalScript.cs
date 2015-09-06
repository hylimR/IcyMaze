using UnityEngine;
using System.Collections;

public class OnPortalScript : MonoBehaviour
{
    public GameObject LeftBottom;
    public GameObject LeftTop;
    public GameObject RightBottom;
    public GameObject RightTop;
    public int level;
    TriggersheetScript l1, l2, r1, r2;
    // Use this for initialization
    void Start()
    {
        l1 = LeftBottom.GetComponent<TriggersheetScript>();
        l2 = LeftTop.GetComponent<TriggersheetScript>();
        r1 = RightBottom.GetComponent<TriggersheetScript>();
        r2 = RightTop.GetComponent<TriggersheetScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompleted())
        {
            Destroy(GameObject.Find(MasterScript.secondScene));
            MasterScript.isSecondSceneCompleted = true;
        }

    }
    private bool isCompleted()
    {
        if (l1.isOn && r2.isOn && l2.isOn && r1.isOn)
        { return true; }
        else
        { return false; }
    }
}