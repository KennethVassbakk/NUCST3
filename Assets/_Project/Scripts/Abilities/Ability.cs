﻿// Author: Kenneth Vassbakk

using System;
using Systems.Cooldown;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : ScriptableObject, ICooldown
    {
        public float aBaseCooldown = 1f;             // Base cooldown of the ability
        public string aDescription;                  // Ability Description
        public string aName;                         // Ability Name
        public string aSoundClip;                    // FMOD Sound String 
        public Sprite aSprite;                       // Ability Icon

        // These are used for calculating the cooldown
        [HideInInspector] public float cooldownDuration;
        [HideInInspector] public float coolDownTimeLeft;
        [HideInInspector] public Guid id;
        [HideInInspector] public float nextReadyTime;
        [HideInInspector] public bool isRangedAttack;

        public Guid Id => id;
        public float CooldownDuration => aBaseCooldown;

        /// <summary>
        ///     Initialization of the  Ability
        /// </summary>
        /// <param name="character">The unit holder this ability</param>
        public abstract void Initialize(GameObject character);

        /// <summary>
        ///     Trigger the ability
        /// </summary>
        public abstract bool TriggerAbility(GameObject character, Transform spawnPoint = null);
    }
}