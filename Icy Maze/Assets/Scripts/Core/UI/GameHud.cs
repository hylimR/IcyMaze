using UnityEngine;
using UnityEngine.UI;

namespace IcyMaze.UI
{
    /// Replaces the fixed-pixel OnGUI boxes, which were positioned with literals like
    /// Screen.width / 2 + 350 and fell off the screen on anything but a 2015 monitor.
    public class GameHud : MonoBehaviour
    {
        const float BannerSeconds = 2.4f;

        Text[] trialRows;
        Text statsText;
        Text hintText;
        Text bannerText;
        GameObject winPanel;
        RawImage winImage;
        Text winSummary;
        AspectRatioFitter winFitter;
        CanvasGroup objectivesGroup;

        float bannerTimer;
        string persistentHint = "";
        int shownSeconds = -1;
        int shownDeaths = -1;

        public static GameHud Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            Build();
            GameProgress.Changed += Refresh;
            SceneFlow.TrialEntered += OnTrialEntered;
            SceneFlow.TrialExited += OnTrialExited;
            Refresh();
        }

        void OnDestroy()
        {
            GameProgress.Changed -= Refresh;
            SceneFlow.TrialEntered -= OnTrialEntered;
            SceneFlow.TrialExited -= OnTrialExited;
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            // Rebuilt only when a displayed value actually changes; formatting this every
            // frame allocates two strings per frame for no visible difference.
            int seconds = Mathf.FloorToInt(GameProgress.ElapsedSeconds);
            if (seconds != shownSeconds || GameProgress.Deaths != shownDeaths)
            {
                shownSeconds = seconds;
                shownDeaths = GameProgress.Deaths;
                statsText.text = $"<color=#9FC7E8>TIME</color>  {GameProgress.FormatElapsed()}    " +
                                 $"<color=#9FC7E8>FALLS</color>  {shownDeaths}";
            }

            if (bannerTimer > 0f)
            {
                bannerTimer -= Time.unscaledDeltaTime;
                Color color = bannerText.color;
                color.a = Mathf.Clamp01(bannerTimer / 0.6f);
                bannerText.color = color;
                if (bannerTimer <= 0f) bannerText.gameObject.SetActive(false);
            }
        }

        public void ShowBanner(string message)
        {
            bannerText.text = message;
            bannerText.color = new Color(UiBuilder.Frost.r, UiBuilder.Frost.g, UiBuilder.Frost.b, 1f);
            bannerText.gameObject.SetActive(true);
            bannerTimer = BannerSeconds;
        }

        public void SetHint(string hint)
        {
            persistentHint = hint ?? "";
            hintText.text = persistentHint;
        }

        public void SetObjectivesVisible(bool visible)
        {
            objectivesGroup.alpha = visible ? 1f : 0f;
        }

        public void SetWinScreen(Texture texture)
        {
            if (texture == null) return;
            winImage.texture = texture;
            winImage.enabled = true;
            winFitter.aspectRatio = texture.height > 0 ? (float)texture.width / texture.height : 16f / 9f;
        }

        public void ShowWinScreen(bool visible)
        {
            if (visible && winSummary != null)
            {
                winSummary.text = $"Time {GameProgress.FormatElapsed()}    Falls {GameProgress.Deaths}";
            }
            winPanel.SetActive(visible);
        }

        void OnTrialEntered(string scene)
        {
            ShowBanner(GameScenes.DisplayName(scene).ToUpperInvariant());
            SetHint(GameScenes.Objective(scene));
            Refresh();
        }

        void OnTrialExited(string scene)
        {
            SetHint(GameScenes.Objective(GameScenes.Hub));
            Refresh();
        }

        void Refresh()
        {
            if (trialRows == null) return;

            if (!GameProgress.RunComplete && winPanel != null) winPanel.SetActive(false);

            for (int i = 0; i < trialRows.Length; i++)
            {
                string trial = GameScenes.Trials[i];
                bool done = GameProgress.IsComplete(trial);
                bool active = SceneFlow.ActiveTrial == trial;
                trialRows[i].text = (done ? "<b>[x]</b>  " : active ? "<b>[>]</b>  " : "[ ]  ") + GameScenes.DisplayName(trial);
                trialRows[i].color = done ? UiBuilder.Cleared : active ? UiBuilder.Ice : UiBuilder.Dim;
            }
        }

        void Build()
        {
            Canvas canvas = UiBuilder.CreateCanvas(transform, "HUD Canvas", 100, true);
            Transform root = canvas.transform;

            RectTransform objectives = UiBuilder.Rect(root, "Objectives", new Vector2(0f, 1f), new Vector2(0f, 1f),
                                                      new Vector2(0f, 1f), new Vector2(28f, -28f), new Vector2(430f, 196f));
            objectivesGroup = objectives.gameObject.AddComponent<CanvasGroup>();
            objectivesGroup.interactable = false;
            objectivesGroup.blocksRaycasts = false;
            Image objectivesBg = UiBuilder.Backdrop(objectives, "Backdrop", UiBuilder.Panel);
            objectivesBg.raycastTarget = false;

            Text title = UiBuilder.Label(objectives, "Title", "ICY MAZE", 30, TextAnchor.UpperLeft, UiBuilder.Ice);
            title.fontStyle = FontStyle.Bold;
            Place(title.rectTransform, new Vector2(20f, -16f), new Vector2(390f, 36f));

            trialRows = new Text[GameScenes.Trials.Length];
            for (int i = 0; i < trialRows.Length; i++)
            {
                trialRows[i] = UiBuilder.Label(objectives, "Trial " + i, "", 24, TextAnchor.UpperLeft, UiBuilder.Dim);
                Place(trialRows[i].rectTransform, new Vector2(20f, -62f - i * 34f), new Vector2(390f, 30f));
            }

            statsText = UiBuilder.Label(root, "Stats", "", 24, TextAnchor.UpperRight, UiBuilder.Frost);
            statsText.rectTransform.anchorMin = new Vector2(1f, 1f);
            statsText.rectTransform.anchorMax = new Vector2(1f, 1f);
            statsText.rectTransform.pivot = new Vector2(1f, 1f);
            statsText.rectTransform.anchoredPosition = new Vector2(-28f, -28f);
            statsText.rectTransform.sizeDelta = new Vector2(420f, 34f);

            hintText = UiBuilder.Label(root, "Hint", "", 26, TextAnchor.LowerCenter, UiBuilder.Frost);
            hintText.rectTransform.anchorMin = new Vector2(0.5f, 0f);
            hintText.rectTransform.anchorMax = new Vector2(0.5f, 0f);
            hintText.rectTransform.pivot = new Vector2(0.5f, 0f);
            hintText.rectTransform.anchoredPosition = new Vector2(0f, 34f);
            hintText.rectTransform.sizeDelta = new Vector2(1100f, 90f);

            bannerText = UiBuilder.Label(root, "Banner", "", 64, TextAnchor.MiddleCenter, UiBuilder.Frost);
            bannerText.fontStyle = FontStyle.Bold;
            bannerText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            bannerText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            bannerText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            bannerText.rectTransform.anchoredPosition = new Vector2(0f, 200f);
            bannerText.rectTransform.sizeDelta = new Vector2(1400f, 120f);
            bannerText.gameObject.SetActive(false);

            BuildWinPanel(root);
        }

        void BuildWinPanel(Transform root)
        {
            RectTransform panel = UiBuilder.Rect(root, "Win Panel", Vector2.zero, Vector2.one,
                                                 new Vector2(0.5f, 0.5f), Vector2.zero, Vector2.zero);
            winPanel = panel.gameObject;
            UiBuilder.Backdrop(panel, "Shade", UiBuilder.Shade);

            RectTransform art = UiBuilder.Rect(panel, "Art", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                                               new Vector2(0.5f, 0.5f), new Vector2(0f, 110f), new Vector2(1100f, 420f));
            winImage = art.gameObject.AddComponent<RawImage>();
            winImage.raycastTarget = false;
            winImage.enabled = false;
            winFitter = art.gameObject.AddComponent<AspectRatioFitter>();
            // Height is fixed by the layout above and the width follows the source art,
            // so the banner never grows into the buttons underneath it.
            winFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            winFitter.aspectRatio = 16f / 9f;

            Text done = UiBuilder.Label(panel, "Done", "ALL THREE TRIALS CLEARED", 44, TextAnchor.MiddleCenter, UiBuilder.Ice);
            done.fontStyle = FontStyle.Bold;
            done.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            done.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            done.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            done.rectTransform.anchoredPosition = new Vector2(0f, -140f);
            done.rectTransform.sizeDelta = new Vector2(1200f, 60f);

            winSummary = UiBuilder.Label(panel, "Summary", "", 28, TextAnchor.MiddleCenter, UiBuilder.Frost);
            Text summary = winSummary;
            summary.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            summary.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            summary.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            summary.rectTransform.anchoredPosition = new Vector2(0f, -196f);
            summary.rectTransform.sizeDelta = new Vector2(1200f, 40f);

            VerticalLayoutGroup buttons = UiBuilder.Column(panel, "Buttons", 16f, new RectOffset(0, 0, 0, 0));
            var buttonsRect = (RectTransform)buttons.transform;
            buttonsRect.anchorMin = new Vector2(0.5f, 0.5f);
            buttonsRect.anchorMax = new Vector2(0.5f, 0.5f);
            buttonsRect.pivot = new Vector2(0.5f, 1f);
            buttonsRect.anchoredPosition = new Vector2(0f, -240f);
            buttonsRect.sizeDelta = new Vector2(340f, 140f);

            UiBuilder.TextButton(buttons.transform, "Play Again", 340f, 58f, SceneFlow.RestartRun);
            UiBuilder.TextButton(buttons.transform, "Quit", 340f, 58f, GameSystems.Quit);

            winPanel.SetActive(false);
        }

        static void Place(RectTransform rect, Vector2 offset, Vector2 size)
        {
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = offset;
            rect.sizeDelta = size;
        }
    }
}
