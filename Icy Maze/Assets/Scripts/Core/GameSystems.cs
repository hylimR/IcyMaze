using IcyMaze.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IcyMaze
{
    /// Persistent host for the HUD, the pause menu and the run timer. Spawned by
    /// GameBootstrap, so it exists no matter which scene you press Play in.
    public class GameSystems : MonoBehaviour
    {
        bool objectivesVisible = true;

        public static GameSystems Instance { get; private set; }

        public GameHud Hud { get; private set; }

        public PauseMenu Pause { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Hud = gameObject.AddComponent<GameHud>();
            Pause = gameObject.AddComponent<PauseMenu>();
            Hud.SetHint(GameScenes.Objective(GameScenes.Hub));
        }

        void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

        void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

        void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        static void OnSceneLoaded(Scene scene, LoadSceneMode mode) => UiBuilder.ReconcileEventSystems();

        void Update()
        {
            if (Instance != this) return;

            GameProgress.Tick(Time.deltaTime);

            if (GameInput.PausePressed && !GameProgress.RunComplete) Pause.Toggle();

            if (GameInput.HelpPressed)
            {
                objectivesVisible = !objectivesVisible;
                Hud.SetObjectivesVisible(objectivesVisible);
            }
        }

        public static void Quit()
        {
            GameProgress.Save();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
