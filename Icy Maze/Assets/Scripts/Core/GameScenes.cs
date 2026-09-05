using System;

namespace IcyMaze
{
    /// Scene and root-object names the original project hard-coded in a dozen places.
    public static class GameScenes
    {
        public const string Hub = "scene_three";
        public const string IceTrial = "scene_four";
        public const string StormTrial = "scene_six";
        public const string FireTrial = "scene_yang";

        /// Name of the root GameObject that carries the hub world inside <see cref="Hub"/>.
        public const string HubRootObject = "Main";

        public static readonly string[] Trials = { IceTrial, StormTrial, FireTrial };

        public static bool IsTrial(string scene) => Array.IndexOf(Trials, scene) >= 0;

        public static string DisplayName(string scene)
        {
            switch (scene)
            {
                case Hub: return "The Icy Maze";
                case IceTrial: return "Trial of Sigils";
                case StormTrial: return "Trial of Storms";
                case FireTrial: return "Trial of Embers";
                default: return scene;
            }
        }

        public static string Objective(string scene)
        {
            switch (scene)
            {
                case IceTrial: return "Slide every rune block onto a magic circle.";
                case StormTrial: return "Light all four trigger sheets. Dodge the thunder.";
                case FireTrial: return "Reach the far end alive. J fires the water gun.";
                default: return "Find a portal and clear the trial beyond it.";
            }
        }
    }
}
