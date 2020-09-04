// Author: John Hauge

using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        private Scene.UI _ui;

        private void Awake()
        {
            _ui = GetComponentInParent<Scene.UI>();
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
