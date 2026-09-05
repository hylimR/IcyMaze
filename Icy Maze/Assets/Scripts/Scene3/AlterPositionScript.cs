using IcyMaze;
using UnityEngine;

/// Lever that slides its four linked blocks between two positions.
public class AlterPositionScript : MonoBehaviour
{
    public GameObject block1, block2, block3, block4;

    AlterableBoxScript[] blocks;
    ActionLatch action;

    void Start()
    {
        blocks = new[]
        {
            Resolve(block1), Resolve(block2), Resolve(block3), Resolve(block4)
        };
    }

    void OnTriggerStay(Collider col)
    {
        if (!PlayerRef.Is(col) || !action.Consume()) return;

        foreach (AlterableBoxScript block in blocks)
        {
            if (block != null) block.Move();
        }
    }

    static AlterableBoxScript Resolve(GameObject go) => go != null ? go.GetComponent<AlterableBoxScript>() : null;
}
