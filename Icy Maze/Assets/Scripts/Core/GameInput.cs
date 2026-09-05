using UnityEngine;

namespace IcyMaze
{
    /// Single entry point for gameplay input. The original scripts polled raw KeyCodes
    /// in eight different files, which locked the game to WASD+K and kept responding
    /// while menus were open.
    public static class GameInput
    {
        /// Cleared while a menu owns the screen so held keys cannot leak into gameplay.
        public static bool GameplayEnabled { get; set; } = true;

        public static Vector2 Move
        {
            get
            {
                if (!GameplayEnabled) return Vector2.zero;
                var move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                return move.sqrMagnitude > 1f ? move.normalized : move;
            }
        }

        /// Interact with switches, tubes and levers.
        public static bool ActionPressed =>
            GameplayEnabled && (Input.GetKeyDown(KeyCode.K) ||
                                Input.GetKeyDown(KeyCode.E) ||
                                Input.GetKeyDown(KeyCode.Space) ||
                                Input.GetKeyDown(KeyCode.JoystickButton0));

        /// Fire the water gun in the ember trial.
        public static bool FirePressed =>
            GameplayEnabled && (Input.GetKeyDown(KeyCode.J) ||
                                Input.GetKeyDown(KeyCode.F) ||
                                Input.GetKeyDown(KeyCode.Mouse0) ||
                                Input.GetKeyDown(KeyCode.JoystickButton2));

        /// Nudge a rune block one tile. Returns Vector2.zero when nothing was tapped.
        public static Vector2 NudgePressed
        {
            get
            {
                if (!GameplayEnabled) return Vector2.zero;
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;
                return Vector2.zero;
            }
        }

        /// Menu keys stay live even when gameplay input is suppressed.
        public static bool PausePressed =>
            Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7);

        public static bool HelpPressed => Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.H);
    }

    /// One-shot guard for action presses read from OnTriggerStay / OnCollisionStay.
    /// Those run once per physics step, so a single tap toggled a switch twice whenever
    /// the frame was long enough to carry two steps -- which is why levers in the 2015
    /// build sometimes appeared to do nothing.
    public struct ActionLatch
    {
        int stamp;

        public bool Consume()
        {
            int now = Time.frameCount + 1;
            if (stamp == now || !GameInput.ActionPressed) return false;
            stamp = now;
            return true;
        }
    }
}
