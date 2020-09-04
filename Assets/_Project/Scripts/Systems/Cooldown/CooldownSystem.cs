// Author: Kenneth Vassbakk

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Systems.Cooldown
{
    
    public class CooldownSystem : MonoBehaviour
    {
        public static CooldownSystem instance;
        private Dictionary<int, CooldownData> _cooldowns = new Dictionary<int, CooldownData>();

        private void Awake()
        {
            if(instance == null)
                instance = this;
        }

        // TODO: Jobify this.
        private void Update() => ProcessCooldowns();

        private void ProcessCooldowns()
        {
            var deltaTime = Time.deltaTime;
            
            foreach (var entry in _cooldowns.Where(entry => entry.Value.Decrementcooldown(deltaTime)).ToArray())
                _cooldowns.Remove(entry.Key);

        }
    
        public void PutOnCooldown(GameObject go, ICooldown cooldown)
        {
            // If its not currently on cooldown, put it on cooldown.
            if(!_cooldowns.ContainsKey(go.GetHashCode() + cooldown.Id.GetHashCode()))
                _cooldowns.Add(go.GetHashCode() + cooldown.Id.GetHashCode(), new CooldownData(cooldown));
        }
        
        public void PutOnCooldown(ICooldown cooldown)
        {
            // If its not currently on cooldown, put it on cooldown.
            if(!_cooldowns.ContainsKey(cooldown.Id.GetHashCode()))
                _cooldowns.Add(cooldown.Id.GetHashCode(), new CooldownData(cooldown));
        }

        public bool IsOnCooldown(GameObject go, Guid id)
        {
            return _cooldowns.ContainsKey(go.GetHashCode() + id.GetHashCode());
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