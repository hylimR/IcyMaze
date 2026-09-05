using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IcyMaze
{
    /// Owns hub <-> trial transitions. The 2015 code did this in four places with
    /// Application.LoadLevelAdditive (removed from Unity years ago) and then threw the
    /// trial's root GameObject away, leaking the additive scene every time.
    public static class SceneFlow
    {
        public static event Action<string> TrialEntered;
        public static event Action<string> TrialExited;

        static GameObject hubRoot;

        public static string ActiveTrial { get; private set; }

        public static bool InTrial => !string.IsNullOrEmpty(ActiveTrial);

        public static bool HasHubRoot => hubRoot != null;

        public static void RegisterHubRoot(GameObject root) => hubRoot = root;

        public static bool EnterTrial(string scene)
        {
            if (InTrial || string.IsNullOrEmpty(scene) || GameProgress.IsComplete(scene)) return false;

            ActiveTrial = scene;
            if (hubRoot != null) hubRoot.SetActive(false);
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            TrialEntered?.Invoke(scene);
            return true;
        }

        public static void CompleteTrial(string scene)
        {
            GameProgress.MarkComplete(scene);
            ExitTrial(scene);
        }

        /// Abandon a trial and return to the hub with no progress recorded. The original
        /// build had no way out of a trial short of solving it or quitting the game.
        public static void AbandonTrial()
        {
            if (InTrial) ExitTrial(ActiveTrial);
        }

        public static void RestartTrial()
        {
            string scene = ActiveTrial;
            if (string.IsNullOrEmpty(scene)) return;

            Time.timeScale = 1f;
            Unload(scene, () =>
            {
                ActiveTrial = scene;
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                TrialEntered?.Invoke(scene);
            });
        }

        public static void RestartRun()
        {
            GameProgress.ResetRun();
            ActiveTrial = null;
            hubRoot = null;
            Time.timeScale = 1f;
            GameInput.GameplayEnabled = true;
            SceneManager.LoadScene(GameScenes.Hub, LoadSceneMode.Single);
        }

        static void ExitTrial(string scene)
        {
            ActiveTrial = null;
            // A trial can be left from its own paused briefing screen; the hub must never
            // inherit that frozen timescale.
            Time.timeScale = 1f;
            Unload(scene, () =>
            {
                if (hubRoot != null) hubRoot.SetActive(true);
                TrialExited?.Invoke(scene);
            });
        }

        static void Unload(string scene, Action then)
        {
            Scene loaded = SceneManager.GetSceneByName(scene);
            if (loaded.IsValid() && loaded.isLoaded && SceneManager.sceneCount > 1)
            {
                AsyncOperation unload = SceneManager.UnloadSceneAsync(loaded);
                if (unload != null)
                {
                    unload.completed += _ => then();
                    return;
                }
            }

            // Played straight from the trial scene rather than through a portal: there is
            // no hub to come back to, so rebuild the run from the hub scene instead.
            if (hubRoot == null && SceneManager.sceneCount <= 1)
            {
                ActiveTrial = null;
                Time.timeScale = 1f;
                SceneManager.LoadScene(GameScenes.Hub, LoadSceneMode.Single);
                return;
            }

            GameObject root = GameObject.Find(scene);
            if (root != null) UnityEngine.Object.Destroy(root);
            then();
        }
    }
}
