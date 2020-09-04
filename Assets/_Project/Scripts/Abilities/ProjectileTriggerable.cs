// Author: Kenneth Vassbakk

using UnityEngine;

namespace Abilities
{
    public class ProjectileTriggerable : MonoBehaviour
    {
        
        public Transform projectileSpawn;

        public void Fire(GameObject projectile)
        {
            // Using PoolManager to save memory usage.
            PoolManager.Spawn(projectile, projectileSpawn.position, Quaternion.identity);
        }
    }
}