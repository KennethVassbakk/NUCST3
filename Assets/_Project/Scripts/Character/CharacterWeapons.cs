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
        private Weapon[] _myWeapon;
        public Transform weaponLocation;

        private void OnEnable()
        {
            _myWeapon = new Weapon[weapon.Length];
            for (int i = 0; i < weapon.Length; i++)
            {
                _myWeapon[i] = Instantiate(weapon[i]);
            }
            var currentWep = _myWeapon[currentWeaponIndex];
            
            currentWeaponObj = PoolManager.Spawn(currentWep.wGameObject, weaponLocation.position, weaponLocation.rotation);
            currentWeaponObj.transform.SetParent(weaponLocation);
            currentWep.Initialize(gameObject, currentWeaponObj);

            var rangedWep  = currentWep as RangedWeapon;
            if( rangedWep != null)
                GetComponent<ProjectileTriggerable>().projectileSpawn = rangedWep.projectileSpawnTransform;
            
        }

        private void Update()
        {
            var ab = _myWeapon[currentWeaponIndex].CurrentAbilities[0];
            // Fire ability just to test it out.
            if (!Input.GetButtonDown("Fire1")) return;
            ab.TriggerAbility();
        }
    }
}