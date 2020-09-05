// Author: Kenneth Vassbakk

using UnityEngine;

namespace Combat
{
    // This is just a test script.
    public class DamageOnContact : MonoBehaviour
    {
        [SerializeField] private int amount = 10;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.DealDamage(amount);
        }
    }
}