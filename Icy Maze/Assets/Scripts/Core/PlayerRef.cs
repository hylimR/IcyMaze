using System.Collections.Generic;
using UnityEngine;

namespace IcyMaze
{
    /// The original scripts identified the player three different ways -- by the name
    /// "unitychan", by the name "subunitychan", and by the tag "Playerchan" -- which is
    /// why several traps silently did nothing in the scenes that used the other spelling.
    /// Players register themselves here instead, and the legacy tag stays as a fallback
    /// so untouched scene data keeps working.
    public static class PlayerRef
    {
        public const string LegacyTag = "Playerchan";

        static readonly HashSet<GameObject> active = new HashSet<GameObject>();

        public static void Register(GameObject player)
        {
            if (player != null) active.Add(player);
        }

        public static void Unregister(GameObject player)
        {
            if (player != null) active.Remove(player);
        }

        public static bool Is(GameObject candidate)
        {
            if (candidate == null) return false;
            return active.Contains(candidate) || candidate.CompareTag(LegacyTag);
        }

        public static bool Is(Component candidate) => candidate != null && Is(candidate.gameObject);

        /// The player currently in control, or null between scene transitions.
        public static GameObject Current
        {
            get
            {
                foreach (GameObject go in active)
                {
                    if (go != null && go.activeInHierarchy) return go;
                }
                return null;
            }
        }
    }
}
