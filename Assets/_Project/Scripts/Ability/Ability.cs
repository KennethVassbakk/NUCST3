// Author: Kenneth Vassbakk

using UnityEngine;

namespace Ability
{
    public abstract class Ability : ScriptableObject
    {
        public float aBaseCooldown = 1f;             // Base cooldown of the ability
        public string aDescription;                  // Ability Description
        public string aName;                         // Ability Name
        public string aSoundClip;                    // FMOD Sound String 
        public Sprite aSprite;                       // Ability Icon

        /// <summary>
        ///     Initialization of the  Ability
        /// </summary>
        /// <param name="obj"></param>
        public abstract void Initialize(GameObject obj);

        /// <summary>
        ///     Trigger the ability
        /// </summary>
        public abstract void TriggerAbility();
    }
}