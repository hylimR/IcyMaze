using IcyMaze;
using UnityEngine;

/// Doorway from the hub into one of the three trials.
public class PortalScript : MonoBehaviour
{
    const float SpentBrightness = 0.1f;

    public string scene;
    public GameObject mainScene;

    LensFlare flare;
    float litBrightness = 1f;

    void Awake()
    {
        flare = GetComponent<LensFlare>();
        if (flare != null) litBrightness = flare.brightness;
    }

    // The original re-read the completion flag and reassigned the flare every frame.
    void OnEnable()
    {
        GameProgress.Changed += Refresh;
        Refresh();
    }

    void OnDisable() => GameProgress.Changed -= Refresh;

    void OnTriggerEnter(Collider col)
    {
        if (!PlayerRef.Is(col) || GameProgress.IsComplete(scene)) return;

        if (!SceneFlow.HasHubRoot && mainScene != null) SceneFlow.RegisterHubRoot(mainScene);
        SceneFlow.EnterTrial(scene);
    }

    void Refresh()
    {
        if (flare != null) flare.brightness = GameProgress.IsComplete(scene) ? SpentBrightness : litBrightness;
    }
}
