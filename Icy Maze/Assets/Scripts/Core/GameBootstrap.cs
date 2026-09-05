using UnityEngine;

namespace IcyMaze
{
    /// Creates the remastered systems without touching a single scene file. The 2015
    /// scenes are preserved byte for byte; everything new bootstraps itself here.
    public static class GameBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Boot()
        {
            GameProgress.Load();
            // A finished run starts over rather than dumping the player straight onto the
            // victory screen the next time they launch.
            if (GameProgress.RunComplete) GameProgress.ResetRun();
            Application.targetFrameRate = -1;
            Time.timeScale = 1f;
            GameInput.GameplayEnabled = true;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void SpawnSystems()
        {
            if (GameSystems.Instance != null) return;
            var host = new GameObject("[Icy Maze Systems]");
            Object.DontDestroyOnLoad(host);
            host.AddComponent<GameSystems>();
        }
    }
}
