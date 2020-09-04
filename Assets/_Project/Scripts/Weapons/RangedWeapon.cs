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

        public override void Initialize(GameObject character)
        {
            foreach (var entry in wAbilities) entry.Initialize(character);
        }
    }
}