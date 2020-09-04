// Author: Kenneth Vassbakk

using UnityEngine;

namespace Character.Controller
{
    public interface IMovementModifier
    {
        Vector3 Value { get; }
    }
}