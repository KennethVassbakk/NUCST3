// Author: Kenneth Vassbakk

using UnityEngine;

namespace Abilities
{
    public class ProjectileTriggerable : MonoBehaviour
    {
        public GameObject Fire(GameObject go, GameObject projectile, Transform spawnLocation)
        {
            // Using PoolManager to save memory usage.
            return PoolManager.Spawn(projectile, spawnLocation.position, spawnLocation.rotation);
        }
    }
}