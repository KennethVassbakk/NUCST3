// Author: John Hauge

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] public GameObject pauseMenuGO;
        private Controls _controls;

        private void Awake()
        {
            _controls = new Controls();
        }
        public void TogglePause()
        {
            pauseMenuGO.SetActive(!pauseMenuGO.activeSelf);
            Time.timeScale = pauseMenuGO.activeSelf ? 0f : 1f;
        }
        
        private void OnEnable()
        {
            _controls.Player.Enable();
            _controls.Player.PauseMenu.performed += ctx => TogglePause();
        }
        
        private void OnDisable()
        {
            
            _controls.Player.Disable();
            
        }
    }
}
