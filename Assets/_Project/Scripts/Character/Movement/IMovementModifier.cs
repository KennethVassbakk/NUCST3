// Author: Kenneth Vassbakk

using UnityEngine;

namespace Character.Movement
{
    public interface IMovementModifier
    {
        Vector3 Value { get; }
    }
}