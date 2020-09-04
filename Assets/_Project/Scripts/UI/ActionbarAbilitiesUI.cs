using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace UI
{
    public class ActionbarAbilitiesUi : MonoBehaviour
    {
        public static ActionbarAbilitiesUi instance;

        // Store the current abilities we have on the bar
        private Dictionary<Ability, GameObject> _currentAbilities = new Dictionary<Ability, GameObject>();
        public Transform abilityIcons;
        public GameObject abilityPrefab;

        private void OnEnable()
        {
            instance = this;
        }

        public void UpdateAbilities(IEnumerable<Ability> abilities)
        {
            // Remove all the current abilities
            
            // Add new abilities
            foreach (var ability in abilities)
                if (_currentAbilities.ContainsKey(ability))
                    // Time to update text here.
                    _currentAbilities[ability].GetComponent<AbilityCooldown>().UpdateValues(ability);
                else
                    AddAbility(ability);
        }

        private void RemoveAbilities()
        {
            if (_currentAbilities.Count == 0)
                return;

            foreach (var entry in _currentAbilities) PoolManager.DeSpawn(entry.Value);

            _currentAbilities = new Dictionary<Ability, GameObject>();
        }

        private void AddAbility(Ability ability)
        {
            var tempAbility = PoolManager.Spawn(abilityPrefab, Vector3.zero, Quaternion.identity);
            tempAbility.transform.SetParent(abilityIcons);
            var abilityCooldown = tempAbility.GetComponent<AbilityCooldown>();
            abilityCooldown.SetProperties(ability);

            _currentAbilities.Add(ability, tempAbility);
        }
    }
}