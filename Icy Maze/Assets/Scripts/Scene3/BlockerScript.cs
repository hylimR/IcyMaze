using System.Collections;
using IcyMaze;
using UnityEngine;

/// Spinning hazard that patrols a four-point loop and shoves the player on contact.
public class BlockerScript : MonoBehaviour
{
    static bool layerCollisionsDisabled;

    public Vector3 pos1, pos2, pos3, pos4;
    public float rate = 1f;

    [SerializeField] float spinDegreesPerSecond = 180f;
    [SerializeField] float knockbackImpulse = 8f;
    [SerializeField] float knockbackCooldown = 0.5f;

    float nextKnockback;

    void Awake()
    {
        if (layerCollisionsDisabled) return;

        int blockerLayer = LayerMask.NameToLayer("Blocker");
        if (blockerLayer >= 0) Physics.IgnoreLayerCollision(blockerLayer, blockerLayer, true);
        layerCollisionsDisabled = true;
    }

    // Start also kicked off the patrol, so every blocker ran two copies of the loop at
    // once and jittered between two targets.
    void OnEnable() => StartCoroutine(Moving());

    void OnDisable() => StopAllCoroutines();

    void Update() => transform.Rotate(Vector3.forward, spinDegreesPerSecond * Time.deltaTime, Space.Self);

    IEnumerator Moving()
    {
        while (true)
        {
            yield return StartCoroutine(Move(pos1));
            yield return StartCoroutine(Move(pos2));
            yield return StartCoroutine(Move(pos3));
            yield return StartCoroutine(Move(pos4));
        }
    }

    IEnumerator Move(Vector3 destination)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Slerp(transform.localPosition, destination, t * rate);
            yield return null;
        }
    }

    void OnCollisionStay(Collision col)
    {
        // The original tested this blocker's own name against the player's, so the shove
        // never fired. Restoring it at the original strength -- a fresh 30-unit impulse on
        // every physics step -- would fire the player out of the level, so it is now one
        // bounded knock with a cooldown.
        if (Time.time < nextKnockback || !PlayerRef.Is(col.gameObject)) return;

        Rigidbody hit = col.rigidbody;
        if (hit == null) return;

        nextKnockback = Time.time + knockbackCooldown;
        Vector3 away = col.transform.position - transform.position;
        away.y = 0f;
        if (away.sqrMagnitude < 0.001f) away = Random.insideUnitSphere;
        hit.AddForce(away.normalized * knockbackImpulse, ForceMode.Impulse);
    }
}
