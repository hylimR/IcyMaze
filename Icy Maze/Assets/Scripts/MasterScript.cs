using IcyMaze;
using IcyMaze.UI;
using UnityEngine;

/// Sits on the hub's "Main" root object. It used to double as the global variable bag;
/// run state now lives in GameProgress and scene transitions in SceneFlow, so this is
/// just the hub's own presenter.
[RequireComponent(typeof(AudioSource))]
public class MasterScript : MonoBehaviour
{
    public const string mainScene = GameScenes.HubRootObject;
    public const string firstScene = GameScenes.IceTrial;
    public const string secondScene = GameScenes.StormTrial;
    public const string thirdScene = GameScenes.FireTrial;

    /// Legacy identifier kept so untouched scene data still resolves; PlayerRef is the
    /// supported way to ask whether something is the player.
    public const string playerName = "unitychan";

    public AudioClip victoryOST;
    public Texture winScreen;

    AudioSource victory;
    bool victoryPlayed;

    public static GameObject Main { get; private set; }

    public static bool IsSceneComplete(string scene) => GameProgress.IsComplete(scene);

    void Awake()
    {
        Main = gameObject;
        SceneFlow.RegisterHubRoot(gameObject);
        victory = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (GameHud.Instance != null)
        {
            GameHud.Instance.SetWinScreen(winScreen);
            GameHud.Instance.SetHint(GameScenes.Objective(GameScenes.Hub));
        }
    }

    void Update()
    {
        if (victoryPlayed || !GameProgress.RunComplete) return;

        victoryPlayed = true;
        GameProgress.Save();

        if (victory != null && victoryOST != null)
        {
            victory.clip = victoryOST;
            victory.Play();
        }

        if (GameHud.Instance != null)
        {
            GameHud.Instance.SetWinScreen(winScreen);
            GameHud.Instance.ShowWinScreen(true);
        }

        GameInput.GameplayEnabled = false;
        Time.timeScale = 0f;
    }
}
