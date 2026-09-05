using IcyMaze;
using UnityEngine;

/// Follow camera. The original snapped to the player in Update, one frame behind the
/// physics step that moved them, which read as a permanent jitter.
public class MainCameraScript : MonoBehaviour
{
    public GameObject player;
    public float height;
    public float x_axis;
    public float z_axis;

    [SerializeField] float baseHeight = 20f;
    [SerializeField, Range(0f, 0.5f)] float smoothTime = 0.08f;

    Vector3 velocity;

    void LateUpdate()
    {
        if (player == null || !player.activeInHierarchy)
        {
            GameObject fallback = PlayerRef.Current;
            if (fallback == null) return;
            player = fallback;
        }

        Vector3 target = player.transform.position + new Vector3(x_axis, baseHeight + height, z_axis);
        transform.position = smoothTime > 0f
            ? Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime, Mathf.Infinity, Time.unscaledDeltaTime)
            : target;
    }
}
