// Author: Kenneth Vassbakk

using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
    public class ProjectileAbility : Ability
    {
        public GameObject projectile;
        public float damage;
        public float hitForce = 100f;            // Force to apply to hit target
        public float range;                      // Range of the ability

#pragma warning disable 649
        private ProjectileTriggerable _projectileTriggerable;
#pragma warning restore 649

        public override void Initialize(GameObject obj)
        {
            _projectileTriggerable = obj.GetComponent<ProjectileTriggerable>();
        }

        public override void TriggerAbility()
        {
            _projectileTriggerable.Fire(projectile);
        }
    }
}