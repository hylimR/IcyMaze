using IcyMaze;
using IcyMaze.UI;
using UnityEngine;

/// Watches the four trigger sheets of the Trial of Storms.
public class OnPortalScript : MonoBehaviour
{
    public GameObject LeftBottom;
    public GameObject LeftTop;
    public GameObject RightBottom;
    public GameObject RightTop;
    public int level;

    TriggersheetScript[] sheets;
    bool finished;

    void Start()
    {
        sheets = new[] { Sheet(LeftBottom), Sheet(LeftTop), Sheet(RightBottom), Sheet(RightTop) };
        if (GameHud.Instance != null) GameHud.Instance.SetHint(GameScenes.Objective(GameScenes.StormTrial));
    }

    void Update()
    {
        // The original kept destroying the scene root and re-showing the hub on every
        // frame after the win, once per frame, forever.
        if (finished || !IsCompleted()) return;

        finished = true;
        SceneFlow.CompleteTrial(GameScenes.StormTrial);
    }

    bool IsCompleted()
    {
        foreach (TriggersheetScript sheet in sheets)
        {
            if (sheet == null || !sheet.isOn) return false;
        }
        return true;
    }

    static TriggersheetScript Sheet(GameObject go) => go != null ? go.GetComponent<TriggersheetScript>() : null;
}
