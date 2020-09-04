// Author: Kenneth Vassbakk

using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "Weapons/RangedWeapon")]
    public class RangedWeapon : Weapon
    {
        // TODO: WeaponParameters
        // We're currently handling this in a weird way perhaps.
        // It might be better to have a WeaponParameters on the Weapon Prefab itself
        // and handing information to/from that.

        public string projectileSpawn;                    // Name of the transform holding the projectile spawn
        private Transform _projectileSpawn;               // Reference to that location.
        
        public override void Initialize(GameObject obj)
        {
            _projectileSpawn = obj.transform.Find(projectileSpawn);
            
            if(_projectileSpawn == null)
                Debug.Log($"Weapon {wName} could not find its projectile spawn location {_projectileSpawn}");
        }
    }
}