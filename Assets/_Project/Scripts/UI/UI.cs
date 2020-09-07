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
            if (pauseMenuGO.activeSelf)
            {
                pauseMenuGO.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pauseMenuGO.SetActive(true);
                Time.timeScale = 0f;
            }
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
