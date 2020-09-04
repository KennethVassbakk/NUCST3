// Author: Kenneth Vassbakk

using System;

namespace Systems.Cooldown
{
    public interface ICooldown 
    {
        Guid  Id { get; }
        float CooldownDuration { get; }
    }
}