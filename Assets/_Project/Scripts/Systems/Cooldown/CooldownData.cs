// Author: Kenneth Vassbakk

using Unity.Mathematics;

namespace Systems.Cooldown
{
    public class CooldownData
    {
        public CooldownData(ICooldown cooldown)
        {
            RemainingTime = cooldown.CooldownDuration;
        }
        
        public float RemainingTime { get; private set; }

        public bool DecrementCooldown(float deltaTime)
        {
            RemainingTime = math.max(RemainingTime - deltaTime, 0f);
            return !(math.abs(RemainingTime) > 0);
        }
    }
}