using System;
using System.Collections;

namespace UnityEngine
{
    public class Component : Object
    {
        public Transform transform => null;
        public GameObject gameObject => null;
        public string tag { get; set; }

        public T GetComponent<T>() => default;
        public Component GetComponent(Type type) => null;
        public Component GetComponent(string type) => null;
        public T GetComponentInParent<T>() => default;
        public T GetComponentInChildren<T>() => default;
        public T[] GetComponentsInChildren<T>() => new T[0];
        public bool CompareTag(string tag) => false;
    }

    public class Behaviour : Component
    {
        public bool enabled { get; set; }
        public bool isActiveAndEnabled => enabled;
    }

    public class MonoBehaviour : Behaviour
    {
        public bool useGUILayout { get; set; }
        public Coroutine StartCoroutine(IEnumerator routine) => null;
        public Coroutine StartCoroutine(string methodName) => null;
        public void StopCoroutine(IEnumerator routine) { }
        public void StopCoroutine(Coroutine routine) { }
        public void StopAllCoroutines() { }
        public void Invoke(string methodName, float time) { }
        public void InvokeRepeating(string methodName, float time, float repeatRate) { }
        public void CancelInvoke() { }
        public void CancelInvoke(string methodName) { }
        public bool IsInvoking(string methodName) => false;
    }

    public sealed class Coroutine { }

    public sealed class WaitForSeconds : IEnumerator
    {
        public WaitForSeconds(float seconds) { }
        public object Current => null;
        public bool MoveNext() => false;
        public void Reset() { }
    }

    public sealed class WaitForSecondsRealtime : IEnumerator
    {
        public WaitForSecondsRealtime(float seconds) { }
        public object Current => null;
        public bool MoveNext() => false;
        public void Reset() { }
    }

    public sealed class WaitForFixedUpdate : IEnumerator
    {
        public object Current => null;
        public bool MoveNext() => false;
        public void Reset() { }
    }

    public sealed class WaitForEndOfFrame : IEnumerator
    {
        public object Current => null;
        public bool MoveNext() => false;
        public void Reset() { }
    }

    public class GameObject : Object
    {
        public GameObject() { }
        public GameObject(string name) { }
        public GameObject(string name, params Type[] components) { }

        public Transform transform => null;
        public int layer { get; set; }
        public string tag { get; set; }
        public bool activeSelf => false;
        public bool activeInHierarchy => false;
        public SceneManagement.Scene scene => default;

        public void SetActive(bool value) { }
        public T GetComponent<T>() => default;
        public Component GetComponent(string type) => null;
        public T GetComponentInParent<T>() => default;
        public T GetComponentInChildren<T>() => default;
        public T AddComponent<T>() where T : Component => default;
        public Component AddComponent(Type componentType) => null;
        public bool CompareTag(string tag) => false;
        public void SendMessage(string methodName) { }
        public void SendMessage(string methodName, object value) { }
        public void SendMessage(string methodName, object value, SendMessageOptions options) { }

        public static GameObject Find(string name) => null;
        public static GameObject FindWithTag(string tag) => null;
        public static GameObject[] FindGameObjectsWithTag(string tag) => new GameObject[0];
    }

    public class Transform : Component, IEnumerable
    {
        public Vector3 position { get; set; }
        public Vector3 localPosition { get; set; }
        public Vector3 eulerAngles { get; set; }
        public Vector3 localEulerAngles { get; set; }
        public Quaternion rotation { get; set; }
        public Quaternion localRotation { get; set; }
        public Vector3 localScale { get; set; }
        public Vector3 forward { get; set; }
        public Vector3 right { get; set; }
        public Vector3 up { get; set; }
        public Transform parent { get; set; }
        public int childCount => 0;

        public void Translate(float x, float y, float z) { }
        public void Translate(Vector3 translation) { }
        public void Translate(Vector3 translation, Space relativeTo) { }
        public void Rotate(float xAngle, float yAngle, float zAngle) { }
        public void Rotate(Vector3 axis, float angle) { }
        public void Rotate(Vector3 axis, float angle, Space relativeTo) { }
        public void SetParent(Transform parent) { }
        public void SetParent(Transform parent, bool worldPositionStays) { }
        public void LookAt(Transform target) { }
        public void LookAt(Vector3 worldPosition) { }
        public void SetSiblingIndex(int index) { }
        public Transform GetChild(int index) => null;
        public Vector3 TransformDirection(Vector3 direction) => direction;
        public Vector3 InverseTransformDirection(Vector3 direction) => direction;
        public IEnumerator GetEnumerator() => null;
    }

    public enum Space { World, Self }

    public enum SendMessageOptions { RequireReceiver, DontRequireReceiver }

    public class Rigidbody : Component
    {
        public Vector3 linearVelocity { get; set; }
        public Vector3 angularVelocity { get; set; }
        public Vector3 position { get; set; }
        public float mass { get; set; }
        public bool isKinematic { get; set; }
        public bool useGravity { get; set; }
        public RigidbodyInterpolation interpolation { get; set; }
        public CollisionDetectionMode collisionDetectionMode { get; set; }
        public RigidbodyConstraints constraints { get; set; }

        public void AddForce(Vector3 force) { }
        public void AddForce(Vector3 force, ForceMode mode) { }
        public void MovePosition(Vector3 position) { }
        public void MoveRotation(Quaternion rot) { }
    }

    [Flags]
    public enum RigidbodyConstraints { None = 0, FreezeRotation = 112 }

    public class Collider : Component
    {
        public bool isTrigger { get; set; }
        public Rigidbody attachedRigidbody => null;
        public Bounds bounds => default;
    }

    public struct Bounds
    {
        public Vector3 center => Vector3.zero;
        public Vector3 size => Vector3.zero;
    }

    public class CapsuleCollider : Collider
    {
        public float height { get; set; }
        public float radius { get; set; }
        public Vector3 center { get; set; }
    }

    public class BoxCollider : Collider { }

    public class Collision
    {
        public Collider collider => null;
        public GameObject gameObject => null;
        public Transform transform => null;
        public Rigidbody rigidbody => null;
    }

    public class Animator : Component
    {
        public float speed { get; set; }
        public void SetFloat(string name, float value) { }
        public void SetFloat(int id, float value) { }
        public void SetFloat(string name, float value, float dampTime, float deltaTime) { }
        public void SetFloat(int id, float value, float dampTime, float deltaTime) { }
        public void SetBool(string name, bool value) { }
        public void SetTrigger(string name) { }
        public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex) => default;
        public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex) => default;
        public bool GetBool(string name) => false;
        public float GetFloat(string name) => 0f;
        public void SetInteger(string name, int value) { }
        public void SetLayerWeight(int layerIndex, float weight) { }
        public void CrossFade(string stateName, float normalizedTransitionDuration) { }
        public void CrossFade(string stateName, float normalizedTransitionDuration, int layer) { }
        public void Play(string stateName) { }
        public bool IsInTransition(int layerIndex) => false;
        public static int StringToHash(string name) => 0;
    }

    public struct AnimatorStateInfo
    {
        public bool IsName(string name) => false;
        public float normalizedTime => 0f;
        public int nameHash => 0;
        public int fullPathHash => 0;
        public int shortNameHash => 0;
        public float length => 0f;
    }

    public class AudioSource : Component
    {
        public AudioClip clip { get; set; }
        public float volume { get; set; }
        public bool loop { get; set; }
        public bool isPlaying => false;
        public void Play() { }
        public void Stop() { }
        public void PlayOneShot(AudioClip clip) { }
        public void PlayOneShot(AudioClip clip, float volumeScale) { }
    }

    public class AudioClip : Object { }

    public static class AudioListener
    {
        public static float volume { get; set; }
    }

    public class Renderer : Component
    {
        public bool enabled { get; set; }
        public Material material { get; set; }
        public Material[] materials { get; set; }
    }

    public class LineRenderer : Renderer
    {
        public int positionCount { get; set; }
        public void SetPosition(int index, Vector3 position) { }
        public void SetPositions(Vector3[] positions) { }
    }

    public class Material : Object
    {
        public Color color { get; set; }
    }

    public class Texture : Object
    {
        public int width => 0;
        public int height => 0;
    }

    public class Texture2D : Texture { }

    public class Font : Object { }

    public class Light : Behaviour
    {
        public Color color { get; set; }
        public float intensity { get; set; }
    }

    public class LensFlare : Behaviour
    {
        public float brightness { get; set; }
        public Color color { get; set; }
    }

    public class Camera : Behaviour
    {
        public static Camera main => null;
        public float fieldOfView { get; set; }
    }

    public class Canvas : Behaviour
    {
        public RenderMode renderMode { get; set; }
        public int sortingOrder { get; set; }
    }

    public enum RenderMode { ScreenSpaceOverlay, ScreenSpaceCamera, WorldSpace }

    public class CanvasRenderer : Component { }

    public class RectTransform : Transform
    {
        public Vector2 anchorMin { get; set; }
        public Vector2 anchorMax { get; set; }
        public Vector2 pivot { get; set; }
        public Vector2 anchoredPosition { get; set; }
        public Vector2 sizeDelta { get; set; }
        public Vector2 offsetMin { get; set; }
        public Vector2 offsetMax { get; set; }
    }

    public class CanvasGroup : Behaviour
    {
        public float alpha { get; set; }
        public bool interactable { get; set; }
        public bool blocksRaycasts { get; set; }
    }

    public enum TextAnchor
    {
        UpperLeft, UpperCenter, UpperRight,
        MiddleLeft, MiddleCenter, MiddleRight,
        LowerLeft, LowerCenter, LowerRight
    }

    [Flags]
    public enum FontStyle { Normal = 0, Bold = 1, Italic = 2, BoldAndItalic = 3 }

    public static class GUI
    {
        public static void Box(Rect position, string text) { }
        public static void Label(Rect position, string text) { }
        public static bool Button(Rect position, string text) => false;
        public static void DrawTexture(Rect position, Texture image) { }
    }
}

namespace UnityEngine
{
    public class AnimationClip : Object
    {
        public float length => 0f;
        public bool legacy { get; set; }
    }

    public class RuntimeAnimatorController : Object { }

    public class Avatar : Object { }

    public class SkinnedMeshRenderer : Renderer
    {
        public void SetBlendShapeWeight(int index, float value) { }
        public float GetBlendShapeWeight(int index) => 0f;
    }
}
