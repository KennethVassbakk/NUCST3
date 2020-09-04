// Author: Kenneth Vassbakk

using UnityEngine;
using Abilities;

namespace Weapons
{
    public abstract class Weapon : ScriptableObject
    {
        public string wName = "Weapon Name";            // Name of the Weapon
        public string wDescription;                     // Weapon Description
        public int wLevel;                              // Weapon Level
        public GameObject wGameObject;                  // Weapon GameObject
        public Ability[] wAbilities;                    // Weapon Abilities
        public Sprite wSprite;                          // Weapon Icon

        public abstract void Initialize(GameObject character, GameObject weapon);
    }
}