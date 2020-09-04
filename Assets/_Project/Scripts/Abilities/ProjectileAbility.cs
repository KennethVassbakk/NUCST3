// Author: Kenneth Vassbakk

using System;
using Systems.Cooldown;
using Character;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
    public class ProjectileAbility : Ability
    {
        public float damage;
        public float hitForce = 100f;            // Force to apply to hit target
        public GameObject projectile;
        public float range;                      // Range of the ability

        public override void Initialize(GameObject character)
        {
            // Create a new ID of this Cooldown, combining the hashcode and the abilityname.
            id = Guid.NewGuid() ;
            isRangedAttack = true;
        }

        public override bool TriggerAbility(GameObject character, Transform spawnLocation = null)
        {
            if (CooldownSystem.instance.IsOnCooldown(character, id)) return false;

            var launchedProjectile = character.GetComponent<CharacterWeapons>().projectileTriggerable.Fire(character,projectile, spawnLocation);
            launchedProjectile.GetComponent<Rigidbody>().AddForce(launchedProjectile.transform.forward * 10, ForceMode.VelocityChange);
            CooldownSystem.instance.PutOnCooldown(character, this);
            
            return true;
        }
        
    }
}