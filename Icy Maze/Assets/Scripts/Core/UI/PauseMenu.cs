using UnityEngine;
using UnityEngine.UI;

namespace IcyMaze.UI
{
    /// The 2015 build had no pause, no restart and no way out of a trial you could not
    /// solve; the only exit was killing the process.
    public class PauseMenu : MonoBehaviour
    {
        const string VolumeKey = "IcyMaze.Volume";

        GameObject panel;
        Button restartTrialButton;
        Button abandonButton;
        Text volumeLabel;
        float resumeTimeScale = 1f;

        public static PauseMenu Instance { get; private set; }

        public bool IsOpen => panel != null && panel.activeSelf;

        void Awake()
        {
            Instance = this;
            AudioListener.volume = Mathf.Clamp01(PlayerPrefs.GetFloat(VolumeKey, 1f));
            Build();
            panel.SetActive(false);
        }

        void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void Toggle()
        {
            if (IsOpen) Close();
            else Open();
        }

        public void Open()
        {
            if (IsOpen) return;
            resumeTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            GameInput.GameplayEnabled = false;
            restartTrialButton.gameObject.SetActive(SceneFlow.InTrial);
            abandonButton.gameObject.SetActive(SceneFlow.InTrial);
            panel.SetActive(true);
        }

        public void Close()
        {
            if (!IsOpen) return;
            panel.SetActive(false);
            // Restores the trial's own pause (scene_four opens on a paused briefing screen)
            // instead of blindly forcing timeScale back to 1.
            Time.timeScale = resumeTimeScale;
            GameInput.GameplayEnabled = true;
        }

        void Build()
        {
            Canvas canvas = UiBuilder.CreateCanvas(transform, "Pause Canvas", 200, true);
            panel = canvas.gameObject;

            Image shade = UiBuilder.Backdrop(canvas.transform, "Shade", UiBuilder.Shade);
            shade.raycastTarget = true;

            RectTransform card = UiBuilder.Rect(canvas.transform, "Card", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                                                new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(720f, 736f));
            Image cardBg = UiBuilder.Backdrop(card, "Backdrop", new Color(0.05f, 0.09f, 0.15f, 0.97f));
            cardBg.raycastTarget = true;

            Text title = UiBuilder.Label(card, "Title", "PAUSED", 46, TextAnchor.MiddleCenter, UiBuilder.Ice);
            title.fontStyle = FontStyle.Bold;
            UiBuilder.PlaceTop(title.rectTransform, 34f, new Vector2(640f, 60f));

            float y = 120f;
            Button resume = UiBuilder.TextButton(card, "Resume", 460f, 56f, Close);
            UiBuilder.PlaceTop((RectTransform)resume.transform, y, new Vector2(460f, 56f));

            y += 70f;
            restartTrialButton = UiBuilder.TextButton(card, "Restart Trial", 460f, 56f, () =>
            {
                Close();
                SceneFlow.RestartTrial();
            });
            UiBuilder.PlaceTop((RectTransform)restartTrialButton.transform, y, new Vector2(460f, 56f));

            y += 70f;
            abandonButton = UiBuilder.TextButton(card, "Return to Maze", 460f, 56f, () =>
            {
                Close();
                SceneFlow.AbandonTrial();
            });
            UiBuilder.PlaceTop((RectTransform)abandonButton.transform, y, new Vector2(460f, 56f));

            y += 70f;
            Button restartRun = UiBuilder.TextButton(card, "Restart Run", 460f, 56f, () =>
            {
                Close();
                SceneFlow.RestartRun();
            });
            UiBuilder.PlaceTop((RectTransform)restartRun.transform, y, new Vector2(460f, 56f));

            y += 70f;
            Button quit = UiBuilder.TextButton(card, "Quit", 460f, 56f, GameSystems.Quit);
            UiBuilder.PlaceTop((RectTransform)quit.transform, y, new Vector2(460f, 56f));

            y += 84f;
            volumeLabel = UiBuilder.Label(card, "Volume Label", "", 24, TextAnchor.MiddleLeft, UiBuilder.Frost);
            UiBuilder.PlaceTop(volumeLabel.rectTransform, y, new Vector2(460f, 30f));

            y += 34f;
            Slider volume = UiBuilder.HorizontalSlider(card, "Volume", 460f, 26f);
            UiBuilder.PlaceTop((RectTransform)volume.transform, y, new Vector2(460f, 26f));
            volume.value = AudioListener.volume;
            volume.onValueChanged.AddListener(OnVolumeChanged);
            OnVolumeChanged(volume.value);

            y += 56f;
            Text controls = UiBuilder.Label(card, "Controls",
                "<b>WASD</b> / arrows / stick   move\n" +
                "<b>K</b> / E / Space   use switches, tubes and levers\n" +
                "<b>J</b> / F   water gun (Trial of Embers)\n" +
                "<b>Esc</b>   pause      <b>F1</b>   toggle objectives",
                22, TextAnchor.UpperCenter, UiBuilder.Dim);
            UiBuilder.PlaceTop(controls.rectTransform, y, new Vector2(640f, 140f));
        }

        void OnVolumeChanged(float value)
        {
            AudioListener.volume = value;
            PlayerPrefs.SetFloat(VolumeKey, value);
            volumeLabel.text = $"VOLUME  {Mathf.RoundToInt(value * 100f)}%";
        }
    }
}
