// Author: Kenneth Vassbakk

using UnityEngine;
using Weapons;

namespace Character
{
    // TODO: Testing Weapon Script.
    public class CharacterWeapons : MonoBehaviour
    {
        public Weapon[] weapon;
        public int currentWeaponIndex;
        public GameObject currentWeaponObj;
        public Transform weaponLocation;

        private void OnEnable()
        {
            currentWeaponObj = PoolManager.Spawn(weapon[currentWeaponIndex].wGameObject, weaponLocation.position, weaponLocation.rotation);
            currentWeaponObj.transform.SetParent(weaponLocation);
        }
    }
}