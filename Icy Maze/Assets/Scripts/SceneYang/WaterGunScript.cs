using UnityEngine;

/// Water bolt. Douses flame boxes on contact.
public class WaterGunScript : MonoBehaviour
{
    const string FireTag = "fire";

    [SerializeField] float speed = 30f;
    [SerializeField] float despawnX = -30f;

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        // Spent bolts used to be parked at (-29.9, 10, 600) instead of destroyed, because
        // the player script kept a live reference to the last one it fired.
        if (transform.position.x <= despawnX) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag(FireTag)) return;

        Destroy(other.collider.gameObject);
        Destroy(gameObject);
    }
}
