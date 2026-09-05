using IcyMaze;
using UnityEngine;

/// Lays out the flame boxes for the Trial of Embers, then drops a second wave in.
public class GameObjectScript : MonoBehaviour
{
    static readonly float[] X = { -3f, -5f, -8f, -10f, -12f, -14f, -14f, -17f, -20f, -23f, -27f, -27f };
    static readonly float[] Z = { -6.8f, -3.3f, 0.5f, -6.8f, 0.5f, -3.3f, -6.8f, 0.5f, -3.3f, -6.8f, 0.5f, -6.8f };
    static readonly float[] XDrop = { -2.7f, -8f, -8f, -17f, -20f, -23f, -27f };
    static readonly float[] ZDrop = { -3.3f, -3.3f, -6.8f, -6.8f, 0.5f, 0.5f, -3.3f };

    public GameObject fireBox;

    [SerializeField] float dropInterval = 1f;

    float timeElapsed = 0.5f;
    int dropped;

    void Start()
    {
        for (int i = 0; i < X.Length; i++)
        {
            Spawner.InSceneOf(fireBox, new Vector3(X[i], -3f, Z[i]), Quaternion.identity, gameObject);
        }
    }

    void Update()
    {
        // The guard was `loop > 7` against a seven-entry table, so the eighth tick indexed
        // past the end and threw every single run.
        if (dropped >= XDrop.Length) return;

        timeElapsed += Time.deltaTime;
        if (timeElapsed < dropInterval) return;

        Spawner.InSceneOf(fireBox, new Vector3(XDrop[dropped], 10f, ZDrop[dropped]), Quaternion.identity, gameObject);
        timeElapsed = 0f;
        dropped++;
    }
}
