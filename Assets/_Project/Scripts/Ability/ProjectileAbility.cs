using System;
using UnityEngine;

namespace Ability
{
    [CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
    public class ProjectileAbility : Ability
    {
        public float damage;
        public float hitForce = 100f;            // Force to apply to hit target
        public float range;                      // Range of the ability

        public override void Initialize(GameObject obj)
        {
            throw new NotImplementedException();
        }

        public override void TriggerAbility()
        {
            throw new NotImplementedException();
        }
    }
}