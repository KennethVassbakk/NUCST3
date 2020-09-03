// Author: John Hauge

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    private Image image;

    private bool fadein;
    private LoadScene load;

    private float t;
    public float delay;

    private void Awake()
    {
        image = GetComponent<Image>();

        //

        t = 1f;
        image.color = new Color(0f, 0f, 0f, 1f);
        fadein = true;
        enabled = true;
    }

    private void FixedUpdate()
    {

        if (fadein)
        {
            t -= Time.deltaTime / delay;
            if (t < 0)
            {
                enabled = false;
                return;
            }
        }
        else
        {
            t += Time.deltaTime / delay;
            if (t > 1)
            {
                enabled = false;
                load.CurtainClosed();
                return;
            }
        }
        image.color = new Color(0f, 0f, 0f, t);
    }


    public void DrawCurtains(LoadScene loadScene)
    {
        t = 0;
        image.color = new Color(0f, 0f, 0f, 0f);
        fadein = false;
        enabled = true;
        load = loadScene;
    }
}
