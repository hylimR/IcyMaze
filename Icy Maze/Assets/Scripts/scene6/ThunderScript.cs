using System.Collections;
using System.Collections.Generic;
using IcyMaze;
using UnityEngine;

/// Telegraphs a lightning pattern with ground markers, then strikes it.
public class ThunderScript : MonoBehaviour
{
    public GameObject thunder;
    public GameObject target;
    public int numberOfThunder = 8;

    [SerializeField] float markSeconds = 1f;
    [SerializeField] float strikeSeconds = 1f;
    [SerializeField] float gapSeconds = 0.5f;

    readonly List<GameObject> spawned = new List<GameObject>();
    readonly List<Vector3> spots = new List<Vector3>();
    Vector3 origin;

    void Awake() => origin = transform.position;

    void OnEnable() => StartCoroutine(Cycle());

    void OnDisable()
    {
        StopAllCoroutines();
        Clear();
    }

    IEnumerator Cycle()
    {
        yield return new WaitForSeconds(gapSeconds);

        while (true)
        {
            SpawnTargets();
            yield return new WaitForSeconds(markSeconds);
            Clear();

            yield return new WaitForSeconds(gapSeconds);
            SpawnThunder();
            yield return new WaitForSeconds(strikeSeconds);
            Clear();

            yield return new WaitForSeconds(gapSeconds);
        }
    }

    void SpawnTargets()
    {
        spots.Clear();
        // numberOfThunder below 8 used to size the position buffer smaller than the eight
        // fixed markers and threw IndexOutOfRange on the first strike.
        int count = Mathf.Max(8, numberOfThunder);

        for (int i = 0; i < 2; i++)
        {
            Mark(new Vector3(origin.x + Random.Range(-11f, -7.5f), origin.y + 0.3f, origin.z + i - 12f));
            Mark(new Vector3(origin.x + Random.Range(7.5f, 11f), origin.y + 0.3f, origin.z + i - 12f));
            Mark(new Vector3(origin.x + Random.Range(-7.5f, 11f), origin.y + 0.3f, origin.z + i + 8f));
            Mark(new Vector3(origin.x + Random.Range(-7.5f, 11f), origin.y + 0.3f, origin.z + i + 8f));
        }

        for (int i = spots.Count; i < count; i++)
        {
            Mark(new Vector3(origin.x + Random.Range(-7.5f, 7.5f), origin.y + 0.3f, origin.z + Random.Range(-7f, 13f)));
        }
    }

    void SpawnThunder()
    {
        foreach (Vector3 spot in spots)
        {
            Track(Spawner.InSceneOf(thunder, spot + new Vector3(0f, 5.7f, 0f), Quaternion.identity, gameObject));
        }
    }

    void Mark(Vector3 spot)
    {
        spots.Add(spot);
        Track(Spawner.InSceneOf(target, spot, Quaternion.identity, gameObject));
    }

    void Track(GameObject instance)
    {
        if (instance != null) spawned.Add(instance);
    }

    /// Tracks what it spawned instead of sweeping the whole scene by tag, which used to
    /// pick up the markers belonging to every other spawner in the level.
    void Clear()
    {
        foreach (GameObject instance in spawned)
        {
            if (instance != null) Destroy(instance);
        }
        spawned.Clear();
    }
}
