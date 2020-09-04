// Author: Kenneth Vassbakk

// TODO: Actually make UI work, this is a mockup

using System.Globalization;
using Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityCooldown : MonoBehaviour
    {
        private Ability _ability;
        private float _cooldownDuration;
        private float _coolDownTimeLeft;
        private Image _myButtonImage;
        private float _nextReadyTime;
        public Text coolDownTextDisplay;
        public Image darkMask;

        private void OnEnable()
        {
            if (_myButtonImage == null)
                _myButtonImage = GetComponent<Image>();

        }

        public void SetProperties(Ability ability)
        {
            _ability = ability;
            _myButtonImage.sprite = ability.aSprite;
            darkMask.sprite = ability.aSprite;
            _cooldownDuration = ability.aBaseCooldown;
        }

        public void UpdateValues(Ability ability)
        {
            var cdLeft = Mathf.Round(ability.coolDownTimeLeft);
            coolDownTextDisplay.text = cdLeft.ToString(CultureInfo.InvariantCulture);
            darkMask.fillAmount = cdLeft / ability.cooldownDuration;
        }

        private void AbilityReady()
        {
            coolDownTextDisplay.enabled = false;
            darkMask.enabled = false;
        }
    }
}