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

        public string projectileSpawn;                        // Name of the transform holding the projectile spawn
        [HideInInspector] public Transform projectileSpawnTransform = null;     // Reference to that location.
        
        public override void Initialize(GameObject character, GameObject weapon)
        {
            InitializeAbilities();
            
            projectileSpawnTransform = weapon.GetComponent<WeaponParameters>().projectileSpawn;
            
            if(projectileSpawnTransform == null)
                Debug.Log($"Weapon {wName} could not find its projectile spawn location {projectileSpawnTransform}");
            
            // We need to initialize all the abilities as well
            foreach (var entry in CurrentAbilities)
            {
                entry.Initialize(character, weapon);
            }
        }
    }
}