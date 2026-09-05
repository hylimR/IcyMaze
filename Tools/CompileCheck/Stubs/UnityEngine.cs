// Minimal stand-ins for the Unity APIs this project touches. Signatures mirror the
// real ones so the type checker rejects anything Unity would also reject.
using System;
using System.Collections;

namespace UnityEngine
{
    public class Object
    {
        public string name { get; set; }
        public HideFlags hideFlags { get; set; }

        public static void Destroy(Object obj) { }
        public static void Destroy(Object obj, float t) { }
        public static void DontDestroyOnLoad(Object target) { }
        public static T Instantiate<T>(T original) where T : Object => default;
        public static T Instantiate<T>(T original, Transform parent) where T : Object => default;
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object => default;
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object => default;
        public static T FindFirstObjectByType<T>() where T : Object => default;
        public static T[] FindObjectsByType<T>(FindObjectsSortMode sortMode) where T : Object => new T[0];
        public static T[] FindObjectsByType<T>(FindObjectsInactive findObjectsInactive, FindObjectsSortMode sortMode) where T : Object => new T[0];

        public static bool operator ==(Object a, Object b) => ReferenceEquals(a, b);
        public static bool operator !=(Object a, Object b) => !ReferenceEquals(a, b);
        public static implicit operator bool(Object exists) => !ReferenceEquals(exists, null);
        public override bool Equals(object other) => ReferenceEquals(this, other);
        public override int GetHashCode() => 0;
    }

    public enum HideFlags { None = 0 }

    public struct Vector2
    {
        public float x, y;
        public Vector2(float x, float y) { this.x = x; this.y = y; }
        public static Vector2 zero => new Vector2(0, 0);
        public static Vector2 one => new Vector2(1, 1);
        public static Vector2 up => new Vector2(0, 1);
        public static Vector2 down => new Vector2(0, -1);
        public static Vector2 left => new Vector2(-1, 0);
        public static Vector2 right => new Vector2(1, 0);
        public float magnitude => 0f;
        public float sqrMagnitude => 0f;
        public Vector2 normalized => this;
        public static Vector2 operator *(Vector2 a, float d) => a;
        public static Vector2 operator /(Vector2 a, float d) => a;
        public static Vector2 operator +(Vector2 a, Vector2 b) => a;
        public static Vector2 operator -(Vector2 a, Vector2 b) => a;
        public static Vector2 operator -(Vector2 a) => a;
        public static bool operator ==(Vector2 a, Vector2 b) => false;
        public static bool operator !=(Vector2 a, Vector2 b) => true;
        public override bool Equals(object other) => false;
        public override int GetHashCode() => 0;
    }

    public struct Vector3
    {
        public float x, y, z;
        public Vector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public static Vector3 zero => new Vector3(0, 0, 0);
        public static Vector3 one => new Vector3(1, 1, 1);
        public static Vector3 up => new Vector3(0, 1, 0);
        public static Vector3 down => new Vector3(0, -1, 0);
        public static Vector3 left => new Vector3(-1, 0, 0);
        public static Vector3 right => new Vector3(1, 0, 0);
        public static Vector3 forward => new Vector3(0, 0, 1);
        public static Vector3 back => new Vector3(0, 0, -1);
        public float magnitude => 0f;
        public float sqrMagnitude => 0f;
        public Vector3 normalized => this;
        public const float kEpsilon = 1e-05f;
        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta) => target;
        public static Vector3 Slerp(Vector3 a, Vector3 b, float t) => b;
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t) => b;
        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity,
                                         float smoothTime, float maxSpeed, float deltaTime) => target;
        public static Vector3 operator *(Vector3 a, float d) => a;
        public static Vector3 operator *(float d, Vector3 a) => a;
        public static Vector3 operator /(Vector3 a, float d) => a;
        public static Vector3 operator +(Vector3 a, Vector3 b) => a;
        public static Vector3 operator -(Vector3 a, Vector3 b) => a;
        public static Vector3 operator -(Vector3 a) => a;
        public static bool operator ==(Vector3 a, Vector3 b) => false;
        public static bool operator !=(Vector3 a, Vector3 b) => true;
        public override bool Equals(object other) => false;
        public override int GetHashCode() => 0;
    }

    public struct Vector4
    {
        public float x, y, z, w;
        public Vector4(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
    }

    public struct Quaternion
    {
        public float x, y, z, w;
        public static Quaternion identity => default;
        public static Quaternion Euler(float x, float y, float z) => default;
        public static Quaternion LookRotation(Vector3 forward) => default;
        public static Quaternion LookRotation(Vector3 forward, Vector3 upwards) => default;
        public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta) => to;
        public void SetLookRotation(Vector3 view) { }
        public void SetLookRotation(Vector3 view, Vector3 up) { }
        public static Quaternion Slerp(Quaternion a, Quaternion b, float t) => b;
        public static Quaternion operator *(Quaternion a, Quaternion b) => a;
        public static Vector3 operator *(Quaternion rotation, Vector3 point) => point;
    }

    public struct Color
    {
        public float r, g, b, a;
        public Color(float r, float g, float b) { this.r = r; this.g = g; this.b = b; a = 1f; }
        public Color(float r, float g, float b, float a) { this.r = r; this.g = g; this.b = b; this.a = a; }
        public static Color white => new Color(1, 1, 1, 1);
        public static Color black => new Color(0, 0, 0, 1);
        public static Color clear => new Color(0, 0, 0, 0);
    }

    public struct Rect
    {
        public Rect(float x, float y, float width, float height) { }
    }

    public class RectOffset
    {
        public RectOffset() { }
        public RectOffset(int left, int right, int top, int bottom) { }
    }

    public static class Mathf
    {
        public const float Infinity = float.PositiveInfinity;
        public const float Deg2Rad = 0.0174532924f;
        public const float Rad2Deg = 57.29578f;
        public static float Abs(float f) => f;
        public static int Max(int a, int b) => a;
        public static float Max(float a, float b) => a;
        public static int Min(int a, int b) => a;
        public static float Min(float a, float b) => a;
        public static float Clamp01(float value) => value;
        public static float Clamp(float value, float min, float max) => value;
        public static int Clamp(int value, int min, int max) => value;
        public static float Round(float f) => f;
        public static int RoundToInt(float f) => 0;
        public static int FloorToInt(float f) => 0;
        public static float Sqrt(float f) => f;
        public static float Lerp(float a, float b, float t) => b;
    }

    public static class Random
    {
        public static float value => 0f;
        public static Vector3 insideUnitSphere => Vector3.zero;
        public static float Range(float min, float max) => min;
        public static int Range(int min, int max) => min;
    }

    public static class Time
    {
        public static float time => 0f;
        public static float deltaTime => 0f;
        public static float fixedDeltaTime => 0.02f;
        public static float unscaledDeltaTime => 0f;
        public static float timeScale { get; set; }
        public static int frameCount => 0;
    }

    public static class Screen
    {
        public static int width => 1920;
        public static int height => 1080;
    }

    public static class Application
    {
        public static int targetFrameRate { get; set; }
        public static void Quit() { }
    }

    public static class PlayerPrefs
    {
        public static int GetInt(string key, int defaultValue = 0) => defaultValue;
        public static float GetFloat(string key, float defaultValue = 0f) => defaultValue;
        public static string GetString(string key, string defaultValue = "") => defaultValue;
        public static void SetInt(string key, int value) { }
        public static void SetFloat(string key, float value) { }
        public static void SetString(string key, string value) { }
        public static void Save() { }
        public static void DeleteAll() { }
    }

    public static class Debug
    {
        public static void Log(object message) { }
        public static void LogWarning(object message) { }
        public static void LogError(object message) { }
    }

    public static class Resources
    {
        public static T GetBuiltinResource<T>(string path) where T : Object => default;
        public static T Load<T>(string path) where T : Object => default;
    }

    public static class Physics
    {
        public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
        {
            hitInfo = default;
            return false;
        }

        public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
        {
            hitInfo = default;
            return false;
        }

        public static bool Raycast(Ray ray, float maxDistance) => false;

        public static bool Raycast(Ray ray, out RaycastHit hitInfo)
        {
            hitInfo = default;
            return false;
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
        {
            hitInfo = default;
            return false;
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance) => false;

        public static void IgnoreLayerCollision(int layer1, int layer2, bool ignore) { }
    }

    public struct Ray
    {
        public Ray(Vector3 origin, Vector3 direction) { }
        public Vector3 origin => Vector3.zero;
        public Vector3 direction => Vector3.zero;
    }

    public struct RaycastHit
    {
        public Collider collider => null;
        public Rigidbody rigidbody => null;
        public Transform transform => null;
        public Vector3 point => Vector3.zero;
        public Vector3 normal => Vector3.zero;
        public float distance => 0f;
    }

    public static class LayerMask
    {
        public static int NameToLayer(string layerName) => 0;
        public static string LayerToName(int layer) => "";
    }

    public enum ForceMode { Force, Acceleration, Impulse, VelocityChange }

    public enum RigidbodyInterpolation { None, Interpolate, Extrapolate }

    public enum CollisionDetectionMode { Discrete, Continuous, ContinuousDynamic, ContinuousSpeculative }

    public enum RuntimeInitializeLoadType { AfterSceneLoad, BeforeSceneLoad, BeforeSplashScreen, AfterAssembliesLoaded, SubsystemRegistration }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RuntimeInitializeOnLoadMethodAttribute : Attribute
    {
        public RuntimeInitializeOnLoadMethodAttribute() { }
        public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType) { }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SerializeFieldAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequireComponent : Attribute
    {
        public RequireComponent(Type requiredComponent) { }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class RangeAttribute : Attribute
    {
        public RangeAttribute(float min, float max) { }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class HeaderAttribute : Attribute
    {
        public HeaderAttribute(string header) { }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TooltipAttribute : Attribute
    {
        public TooltipAttribute(string tooltip) { }
    }
}

namespace UnityEngine
{
    public enum FindObjectsInactive { Exclude, Include }

    public enum FindObjectsSortMode { None, InstanceID }
}
