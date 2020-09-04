// Author: John Hauge

using UnityEngine;
using UnityEngine.Serialization;

namespace Scene
{
    public class UI : MonoBehaviour
    {
        [FormerlySerializedAs("PauseMenu_GO")] public GameObject pauseMenuGO;

        private void Update()
        {
            //to be hooked up to the new input system.

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
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

    }
}
