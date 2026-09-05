using System;
using System.Collections.Generic;
using UnityEngine;

namespace IcyMaze
{
    /// Run state for a play-through. The 2015 build kept this in mutable statics on
    /// MasterScript, so finishing the game left the process in a won state forever and
    /// there was no way to replay without relaunching. Everything here is resettable
    /// and survives a quit through PlayerPrefs.
    public static class GameProgress
    {
        const string SaveKeyPrefix = "IcyMaze.Trial.";
        const string DeathsKey = "IcyMaze.Deaths";
        const string TimeKey = "IcyMaze.Elapsed";

        static readonly HashSet<string> completed = new HashSet<string>();

        /// Raised whenever a trial is cleared or the run is reset, so the HUD never polls.
        public static event Action Changed;

        public static int Deaths { get; private set; }

        /// Seconds of unpaused play in the current run.
        public static float ElapsedSeconds { get; private set; }

        public static int CompletedCount => completed.Count;

        public static bool RunComplete => completed.Count >= GameScenes.Trials.Length;

        public static bool IsComplete(string scene) => !string.IsNullOrEmpty(scene) && completed.Contains(scene);

        public static void MarkComplete(string scene)
        {
            if (string.IsNullOrEmpty(scene) || !completed.Add(scene)) return;
            Save();
            Changed?.Invoke();
        }

        public static void RecordDeath()
        {
            Deaths++;
            Changed?.Invoke();
        }

        public static void Tick(float unscaledDelta)
        {
            if (RunComplete) return;
            ElapsedSeconds += unscaledDelta;
        }

        public static void ResetRun()
        {
            completed.Clear();
            Deaths = 0;
            ElapsedSeconds = 0f;
            Save();
            Changed?.Invoke();
        }

        public static void Load()
        {
            completed.Clear();
            foreach (string trial in GameScenes.Trials)
            {
                if (PlayerPrefs.GetInt(SaveKeyPrefix + trial, 0) == 1) completed.Add(trial);
            }
            Deaths = PlayerPrefs.GetInt(DeathsKey, 0);
            ElapsedSeconds = PlayerPrefs.GetFloat(TimeKey, 0f);
            Changed?.Invoke();
        }

        public static void Save()
        {
            foreach (string trial in GameScenes.Trials)
            {
                PlayerPrefs.SetInt(SaveKeyPrefix + trial, completed.Contains(trial) ? 1 : 0);
            }
            PlayerPrefs.SetInt(DeathsKey, Deaths);
            PlayerPrefs.SetFloat(TimeKey, ElapsedSeconds);
            PlayerPrefs.Save();
        }

        public static string FormatElapsed()
        {
            int total = Mathf.Max(0, Mathf.FloorToInt(ElapsedSeconds));
            return $"{total / 60:00}:{total % 60:00}";
        }
    }
}
