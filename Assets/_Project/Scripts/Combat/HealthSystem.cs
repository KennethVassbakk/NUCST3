// Author: Kenneth Vassbakk

using Unity.Mathematics;
using UnityEngine;

namespace Combat
{
    public class HealthSystem : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private int maxHealth;
        private int _currentHealth;

        private void OnEnable()
        {
            _currentHealth = maxHealth;
        }

        public void DealDamage(int amount)
        {
            if (amount <= 0) return;
            
            _currentHealth = math.max(0, _currentHealth - amount);

            if (_currentHealth == 0)
                PoolManager.DeSpawn(gameObject);
            
        }

        public void Heal(int amount)
        {
            if (amount <= 0) return;
            
            _currentHealth = math.min(maxHealth, _currentHealth + amount);
        }
    }
}