namespace UnityEngine
{
    public enum KeyCode
    {
        None, Backspace, Tab, Return, Escape, Space,
        A, B, C, D, E, F, G, H, I, J, K, L, M,
        N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        UpArrow, DownArrow, LeftArrow, RightArrow,
        LeftShift, RightShift, LeftControl, RightControl,
        LeftAlt, RightAlt, LeftCommand, RightCommand,
        F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
        Mouse0, Mouse1, Mouse2,
        JoystickButton0, JoystickButton1, JoystickButton2, JoystickButton3,
        JoystickButton4, JoystickButton5, JoystickButton6, JoystickButton7
    }

    public static class Input
    {
        public static Vector3 mousePosition => Vector3.zero;
        public static bool anyKey => false;
        public static bool anyKeyDown => false;

        public static float GetAxis(string axisName) => 0f;
        public static float GetAxisRaw(string axisName) => 0f;
        public static bool GetButton(string buttonName) => false;
        public static bool GetButtonDown(string buttonName) => false;
        public static bool GetButtonUp(string buttonName) => false;
        public static bool GetKey(KeyCode key) => false;
        public static bool GetKey(string name) => false;
        public static bool GetKeyDown(KeyCode key) => false;
        public static bool GetKeyDown(string name) => false;
        public static bool GetKeyUp(KeyCode key) => false;
        public static bool GetKeyUp(string name) => false;
        public static bool GetMouseButton(int button) => false;
        public static bool GetMouseButtonDown(int button) => false;
        public static bool GetMouseButtonUp(int button) => false;
    }

    public static class GUILayout
    {
        public static bool Button(string text) => false;
        public static void Label(string text) { }
        public static void BeginArea(Rect screenRect) { }
        public static void EndArea() { }
    }
}
