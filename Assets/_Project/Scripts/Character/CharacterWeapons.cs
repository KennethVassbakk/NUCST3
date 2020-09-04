// Author: Kenneth Vassbakk

using Abilities;
using UnityEngine;
using Weapons;

namespace Character
{
    // TODO: Testing Weapon Script.
    public class CharacterWeapons : MonoBehaviour
    {
        public int currentWeaponIndex;
        public GameObject currentWeaponObj;
        public Weapon[] weapon;
        public Transform weaponLocation;
        public WeaponParameters weaponParameters;

        public ProjectileTriggerable projectileTriggerable;

        private void OnEnable()
        { 
            var currentWep = weapon[currentWeaponIndex];
            projectileTriggerable = GetComponent<ProjectileTriggerable>();
            
            currentWeaponObj = PoolManager.Spawn(currentWep.wGameObject, weaponLocation.position, weaponLocation.rotation);
            currentWeaponObj.transform.SetParent(weaponLocation);
            weaponParameters = currentWeaponObj.GetComponent<WeaponParameters>();
            currentWep.Initialize(gameObject);
        }

        private void Update()
        {
            var ab = weapon[currentWeaponIndex].wAbilities[0];
            // Fire ability just to test it out.
            if (!Input.GetButtonDown("Fire1")) return;
            if (ab.isRangedAttack)
                ab.TriggerAbility(gameObject, weaponParameters.projectileSpawn);
            
            ab.TriggerAbility(gameObject);
        }
    }
}