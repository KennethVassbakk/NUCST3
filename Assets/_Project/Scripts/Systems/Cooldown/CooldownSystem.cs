﻿// Author: Kenneth Vassbakk

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Systems.Cooldown
{
    
    public class CooldownSystem : MonoBehaviour
    {
        public static CooldownSystem instance;

        // Using dictionary, since its slightly faster than lists when doing lookkups often.
        private readonly Dictionary<int, CooldownData> _cooldowns = new Dictionary<int, CooldownData>();

        private void Awake()
        {
            if(instance == null)
                instance = this;
        }

        // TODO: Jobify this.
        private void Update()
        {
            ProcessCooldowns();
        }

        private void ProcessCooldowns()
        {
            var deltaTime = Time.deltaTime;
            
            // Reverse loop through the dictionary since we are modifying it!
            for (var i = _cooldowns.Count - 1; i >= 0; i--)
            {
                var entry = _cooldowns.ElementAt(i);
                if (entry.Value.DecrementCooldown(deltaTime))
                    _cooldowns.Remove(entry.Key);

            }
            
            /*
            foreach (var entry in _cooldowns.Where(entry => entry.Value.DecrementCooldown(deltaTime)).ToArray())
                _cooldowns.Remove(entry.Key);
                */

        }

        public void PutOnCooldown(GameObject go, ICooldown cooldown)
        {
            var key = go.GetHashCode() + cooldown.Id.GetHashCode();
            if(!_cooldowns.ContainsKey(key))
                _cooldowns.Add(key, new CooldownData(cooldown));
        }

        public void PutOnCooldown(ICooldown cooldown)
        {
            var key = cooldown.Id.GetHashCode();
            if(!_cooldowns.ContainsKey(key))
                _cooldowns.Add(key, new CooldownData(cooldown));
        }

        public bool IsOnCooldown(GameObject go, Guid id) 
        {
            return _cooldowns.ContainsKey(go.GetHashCode() + id.GetHashCode());
        }

        public bool IsOnCooldown(GameObject go, ICooldown cooldown) 
        {
            return _cooldowns.ContainsKey(go.GetHashCode() + cooldown.Id.GetHashCode());
        }

        public bool IsOnCooldown(Guid id)
        {
            return _cooldowns.ContainsKey(id.GetHashCode());
        }

        public bool IsOnCooldown(ICooldown cooldown)
        {
            return _cooldowns.ContainsKey(cooldown.Id.GetHashCode());
        }

        public float GetRemainingDuration(Guid id)
        {
            return _cooldowns.ContainsKey(id.GetHashCode()) ? _cooldowns[id.GetHashCode()].RemainingTime : 0f;
        }

        public float GetRemainingDuration(GameObject go, ICooldown cooldown)
        {
            return _cooldowns.ContainsKey(go.GetHashCode() + cooldown.Id.GetHashCode()) ? _cooldowns[cooldown.Id.GetHashCode()].RemainingTime : 0f;
        }

        public float GetRemainingDuration(ICooldown cooldown)
        {
            return _cooldowns.ContainsKey(cooldown.Id.GetHashCode()) ? _cooldowns[cooldown.Id.GetHashCode()].RemainingTime : 0f;
        }
    }
}