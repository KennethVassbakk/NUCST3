// Author: John Hauge

using System;
using UnityEngine;

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
            print("It has been pushed");
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
            _controls.Player.PauseMenu.performed += context => TogglePause();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }
    }
}
