using System;

namespace UnityEngine.SceneManagement
{
    public struct Scene
    {
        public string name => "";
        public int buildIndex => 0;
        public bool isLoaded => false;
        public bool IsValid() => false;
        public static bool operator ==(Scene a, Scene b) => false;
        public static bool operator !=(Scene a, Scene b) => true;
        public override bool Equals(object other) => false;
        public override int GetHashCode() => 0;
    }

    public enum LoadSceneMode { Single, Additive }

    public static class SceneManager
    {
        public static event System.Action<Scene, LoadSceneMode> sceneLoaded;
        public static event System.Action<Scene> sceneUnloaded;
        public static int sceneCount => 1;
        public static int sceneCountInBuildSettings => 0;
        public static Scene GetActiveScene() => default;
        public static Scene GetSceneByName(string name) => default;
        public static void LoadScene(string sceneName) { }
        public static void LoadScene(int sceneBuildIndex) { }
        public static void LoadScene(string sceneName, LoadSceneMode mode) { }
        public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode) => null;
        public static AsyncOperation UnloadSceneAsync(Scene scene) => null;
        public static AsyncOperation UnloadSceneAsync(string sceneName) => null;
        public static void MoveGameObjectToScene(GameObject go, Scene scene) { }
    }
}

namespace UnityEngine
{
    public class AsyncOperation
    {
        public bool isDone => false;
        public float progress => 0f;
        public bool allowSceneActivation { get; set; }
        public event Action<AsyncOperation> completed;
    }
}

namespace UnityEngine.Events
{
    public delegate void UnityAction();

    public delegate void UnityAction<T0>(T0 arg0);

    public class UnityEventBase { }

    public class UnityEvent : UnityEventBase
    {
        public void AddListener(UnityAction call) { }
        public void RemoveListener(UnityAction call) { }
        public void RemoveAllListeners() { }
        public void Invoke() { }
    }

    public class UnityEvent<T0> : UnityEventBase
    {
        public void AddListener(UnityAction<T0> call) { }
        public void RemoveListener(UnityAction<T0> call) { }
        public void RemoveAllListeners() { }
        public void Invoke(T0 arg0) { }
    }
}

namespace UnityEngine.EventSystems
{
    public class UIBehaviour : MonoBehaviour { }

    public class EventSystem : UIBehaviour
    {
        public static EventSystem current => null;
    }

    public class BaseInputModule : UIBehaviour { }

    public class PointerInputModule : BaseInputModule { }

    public class StandaloneInputModule : PointerInputModule { }
}
