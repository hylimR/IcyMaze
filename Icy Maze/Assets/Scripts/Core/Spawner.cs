using UnityEngine;
using UnityEngine.SceneManagement;

namespace IcyMaze
{
    public static class Spawner
    {
        /// Instantiate into the owner's own scene. Trials run as additive scenes while the
        /// hub stays the active one, so plain Instantiate dropped every fireball, firebox
        /// and lightning bolt into the hub instead -- where they outlived the trial that
        /// spawned them.
        public static GameObject InSceneOf(GameObject prefab, Vector3 position, Quaternion rotation, GameObject owner)
        {
            if (prefab == null) return null;

            GameObject instance = Object.Instantiate(prefab, position, rotation);
            if (owner != null && owner.scene.IsValid() && instance.scene != owner.scene)
            {
                SceneManager.MoveGameObjectToScene(instance, owner.scene);
            }
            return instance;
        }
    }
}
