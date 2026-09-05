// Only reachable from code guarded by #if UNITY_EDITOR. The editor-flavoured build
// (CompileCheck.Editor.csproj) defines that symbol so those branches get type-checked too.
namespace UnityEditor
{
    public static class EditorApplication
    {
        public static bool isPlaying { get; set; }
        public static bool isPaused { get; set; }
    }
}
