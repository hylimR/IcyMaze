using IcyMaze;
using IcyMaze.UI;
using UnityEngine;
using UnityEngine.UI;

/// Runs the Trial of Sigils: a briefing screen, then four rune blocks to park on four
/// magic circles.
public class CMasterScript : MonoBehaviour
{
    public GameObject instruction;
    public GameObject startButton, showInstructButton;
    public GameObject mCircle1, mCircle2, mCircle3, mCircle4;

    MagicCircleScript[] circles;
    bool finishing;

    void Start()
    {
        // mCircle3 was read from mCircle4, so the third circle never had to be covered
        // and the puzzle could be finished with one block left over.
        circles = new[] { Circle(mCircle1), Circle(mCircle2), Circle(mCircle3), Circle(mCircle4) };

        Bind(startButton);
        Bind(showInstructButton);
        if (showInstructButton != null) showInstructButton.SetActive(false);

        if (instruction != null) instruction.SetActive(true);
        Time.timeScale = 0f;
        GameInput.GameplayEnabled = false;

        if (GameHud.Instance != null) GameHud.Instance.SetHint(GameScenes.Objective(GameScenes.IceTrial));
    }

    void Update()
    {
        // The original queued a fresh Invoke every frame once the puzzle was solved.
        if (finishing || !IsPuzzleComplete()) return;

        finishing = true;
        Invoke(nameof(FinishGame), 0.5f);
    }

    bool IsPuzzleComplete()
    {
        foreach (MagicCircleScript circle in circles)
        {
            if (circle == null || !circle.isSteppedOn) return false;
        }
        return true;
    }

    void ToggleInstruction()
    {
        bool showing = instruction != null && instruction.activeSelf;
        if (instruction != null) instruction.SetActive(!showing);
        if (showInstructButton != null) showInstructButton.SetActive(showing);
        Time.timeScale = showing ? 1f : 0f;
        GameInput.GameplayEnabled = showing;
    }

    void FinishGame() => SceneFlow.CompleteTrial(GameScenes.IceTrial);

    void Bind(GameObject host)
    {
        Button button = host != null ? host.GetComponent<Button>() : null;
        if (button != null) button.onClick.AddListener(ToggleInstruction);
    }

    static MagicCircleScript Circle(GameObject go) => go != null ? go.GetComponent<MagicCircleScript>() : null;
}
