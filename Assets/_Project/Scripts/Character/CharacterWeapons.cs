﻿// Author: Kenneth Vassbakk

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

        private void OnEnable()
        {
            var currentWep = weapon[currentWeaponIndex];
            
            currentWeaponObj = PoolManager.Spawn(currentWep.wGameObject, weaponLocation.position, weaponLocation.rotation);
            currentWeaponObj.transform.SetParent(weaponLocation);
            currentWep.Initialize(gameObject, currentWeaponObj);

            var rangedWep  = currentWep as RangedWeapon;
            if( rangedWep != null)
                GetComponent<ProjectileTriggerable>().projectileSpawn = rangedWep.projectileSpawnTransform;
            
        }

        private void Update()
        {
            var ab = weapon[currentWeaponIndex].wAbilities[0];
            // Fire ability just to test it out.
            if (!Input.GetButtonDown("Fire1")) return;
            ab.TriggerAbility();
        }
    }
}