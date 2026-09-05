using IcyMaze;
using IcyMaze.UI;
using UnityEngine;

/// Player rules for the Trial of Embers.
public class PlayerScript : MonoBehaviour
{
    const string FireTag = "fire";
    const string FinishName = "Finish";

    public GameObject fireBall;
    public GameObject waterGun;
    public int bulletNo = 3;

    [SerializeField] Vector3 muzzleOffset = new Vector3(-2f, 0f, 0f);
    [SerializeField] Vector3 trapSpawn = new Vector3(-33f, -4.2f, -1.2f);
    [SerializeField] Vector3 trap2Spawn = new Vector3(-33f, -4.2f, -5f);

    Vector3 oriPosition;
    Rigidbody body;
    bool finished;

    void Start()
    {
        oriPosition = transform.localPosition;
        body = GetComponent<Rigidbody>();
        ReportAmmo();
    }

    void Update()
    {
        if (!GameInput.FirePressed || bulletNo <= 0) return;

        // The original assigned the spawned bolt back over the prefab field, so every shot
        // after the first was a copy of the previous bolt wherever it happened to be.
        Spawner.InSceneOf(waterGun, transform.position + muzzleOffset, Quaternion.identity, gameObject);
        bulletNo--;
        ReportAmmo();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag(FireTag)) return;

        transform.localPosition = oriPosition;
        if (body != null)
        {
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
        GameProgress.RecordDeath();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Trap")
        {
            Spawner.InSceneOf(fireBall, trapSpawn, Quaternion.identity, gameObject);
        }
        else if (other.name == "Trap2")
        {
            Spawner.InSceneOf(fireBall, trap2Spawn, Quaternion.identity, gameObject);
        }
        else if (other.name == FinishName && !finished)
        {
            finished = true;
            SceneFlow.CompleteTrial(GameScenes.FireTrial);
        }
    }

    void ReportAmmo()
    {
        if (GameHud.Instance == null) return;

        GameHud.Instance.SetHint($"{GameScenes.Objective(GameScenes.FireTrial)}   ({bulletNo} shot{(bulletNo == 1 ? "" : "s")} left)");
    }
}
