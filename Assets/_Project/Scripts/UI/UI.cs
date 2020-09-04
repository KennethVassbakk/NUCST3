// Author: John Hauge

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject PauseMenu_GO;

    void Update()
    {
       //to be hooked up to the new input system.

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (PauseMenu_GO.activeSelf)
        {
            PauseMenu_GO.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            PauseMenu_GO.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}
