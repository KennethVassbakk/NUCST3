// Author: Kenneth Vassbakk

using Abilities;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : ScriptableObject
    {
        public Ability[] wAbilities; // Weapon Abilities
        public string wDescription; // Weapon Description
        public GameObject wGameObject; // Weapon GameObject
        public int wLevel; // Weapon Level
        public string wName = "Weapon Name"; // Name of the Weapon
        public Sprite wSprite; // Weapon Icon

        public abstract void Initialize(GameObject character);
    }
}