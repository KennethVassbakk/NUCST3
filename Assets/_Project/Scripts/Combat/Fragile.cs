// Author: Kenneth Vassbakk

using UnityEngine;

namespace Combat
{
    public class Fragile : MonoBehaviour, IDamageable
    {
        public void DealDamage(int amount) => PoolManager.DeSpawn(gameObject);
        
        [SerializeField] private bool triggerOnCharacter = true;

        private void OnTriggerEnter(Collider other)
        {
            if (!triggerOnCharacter) return;
            
            other.TryGetComponent<CharacterController>(out var characterController);
            
            if(characterController)
                DealDamage(10);
        }
    }
}