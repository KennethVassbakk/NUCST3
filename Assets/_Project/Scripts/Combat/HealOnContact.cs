// Author: Kenneth Vassbakk
using UnityEngine;

namespace Combat
{
    public class HealOnContact : MonoBehaviour
    {
        // This is just a test script.
        [SerializeField] private int amount = 10;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IHealable>(out var healable))
            {
                healable.Heal(amount);
            }
        }
        
    }
}