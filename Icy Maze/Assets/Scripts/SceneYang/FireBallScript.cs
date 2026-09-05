using IcyMaze;
using UnityEngine;

/// Fireball launched by the pressure traps. It travels along +X and burns out at the
/// end of the corridor.
public class FireBallScript : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] float despawnX = 1.5f;

    // Translate(0.5f, 0, 0) with no delta time: correct on the 60 Hz monitor this was
    // built on, two and a half times too fast on a 144 Hz one.
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0f, 0f);
        if (transform.position.x >= despawnX) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (PlayerRef.Is(other.gameObject)) Destroy(gameObject);
    }
}
