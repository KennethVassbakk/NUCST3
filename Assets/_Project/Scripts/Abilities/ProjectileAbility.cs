// Author: Kenneth Vassbakk

using System;
using Systems.Cooldown;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
    public class ProjectileAbility : Ability
    {
        private ProjectileTriggerable _projectileTriggerable;
        public float damage;
        public float hitForce = 100f;            // Force to apply to hit target
        public GameObject projectile;
        public float range;                      // Range of the ability

        public override void Initialize(GameObject character, GameObject weapon)
        {
            _projectileTriggerable = character.GetComponent<ProjectileTriggerable>();
            
            // Create a new ID of this Cooldown, combining the hashcode and the abilityname.
            id = Guid.NewGuid();
        }

        public override bool TriggerAbility()
        {
            if (CooldownSystem.instance.IsOnCooldown(this)) return false;

            var launchedProjectile = _projectileTriggerable.Fire(projectile);
            launchedProjectile.GetComponent<Rigidbody>().AddForce(launchedProjectile.transform.forward * 10, ForceMode.VelocityChange);
            CooldownSystem.instance.PutOnCooldown(this);
            
            return true;
        }
    }
}