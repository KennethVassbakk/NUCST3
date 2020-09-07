// Author: John Hauge

using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        private UI _ui;

        private void Awake()
        {
            _ui = GetComponentInParent<UI>();
        }

        public void ToGame()
        {
            _ui.TogglePause();
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
