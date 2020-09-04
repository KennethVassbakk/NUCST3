// Author: Kenneth Vassbakk

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Cooldown
{
    public class CooldownSystem : MonoBehaviour
    {
        public static CooldownSystem instance;
        private Dictionary<Guid, CooldownData> _cooldowns = new Dictionary<Guid, CooldownData>();

        private void Awake()
        {
            if(instance == null)
                instance = this;
        }

        private void Update()
        {
            ProcessCooldowns();
        }

        private void ProcessCooldowns()
        {
            var deltaTime = Time.deltaTime;
            
            foreach (var entry in _cooldowns.Where(entry => entry.Value.Decrementcooldown(deltaTime)).ToArray())
                _cooldowns.Remove(entry.Key);

        }

        public void PutOnCooldown(ICooldown cooldown)
        {
            // If its not currently on cooldown, put it on cooldown.
            if(!_cooldowns.ContainsKey(cooldown.Id))
                _cooldowns.Add(cooldown.Id, new CooldownData(cooldown));
        }

        public bool IsOnCooldown(Guid id)
        {
            return _cooldowns.ContainsKey(id);
        }
        
        public bool IsOnCooldown(ICooldown cooldown)
        {
            return _cooldowns.ContainsKey(cooldown.Id);
        }
        
        public float GetRemainingDuration(Guid id)
        {
            return _cooldowns.ContainsKey(id) ? _cooldowns[id].RemainingTime : 0f;
        }

        public float GetRemainingDuration(ICooldown cooldown)
        {
            return _cooldowns.ContainsKey(cooldown.Id) ? _cooldowns[cooldown.Id].RemainingTime : 0f;
        }
    }
    
    public class CooldownData
    {
        public CooldownData(ICooldown cooldown)
        {
            RemainingTime = cooldown.CooldownDuration;
        }
        
        public float RemainingTime { get; private set; }

        public bool Decrementcooldown(float deltaTime)
        {
            RemainingTime = math.max(RemainingTime - deltaTime, 0f);
            return !(Math.Abs(RemainingTime) > 0);
        }
    }
}