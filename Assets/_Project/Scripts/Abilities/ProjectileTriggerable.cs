// Author: Kenneth Vassbakk

using UnityEngine;

namespace Abilities
{
    public class ProjectileTriggerable : MonoBehaviour
    {
        public Transform projectileSpawn;

        public GameObject Fire(GameObject projectile)
        {
            // Using PoolManager to save memory usage.
            return PoolManager.Spawn(projectile, projectileSpawn.position, projectileSpawn.rotation);
        }
    }
}