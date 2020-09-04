// Author: John Hauge

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private UI ui;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
    }

    public void toGame()
    {
        ui.TogglePause();
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
