// Author: Kenneth Vassbakk

using UnityEngine;
using Weapons;

namespace Character
{
    // TODO: Testing Weapon Script.
    public class CharacterWeapons : MonoBehaviour
    {
        public Weapon[] weapon;
        public int _currentWeapon = 0;
        public GameObject currentWeaponObj;
        public Transform weaponLocation;

        private void OnEnable()
        {
            currentWeaponObj = PoolManager.Spawn(weapon[_currentWeapon].wGameObject, weaponLocation.position, weaponLocation.rotation);
            currentWeaponObj.transform.SetParent(weaponLocation);
        }
    }
}