// Author: John Hauge

using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SceneTransition : MonoBehaviour
    {
        private Image _image;

        private bool _fadein;
        private LoadScene _load;

        private float _t;
        public float delay;

        private void Awake()
        {
            _image = GetComponent<Image>();

            //

            _t = 1f;
            _image.color = new Color(0f, 0f, 0f, 1f);
            _fadein = true;
            enabled = true;
        }

        private void FixedUpdate()
        {

            if (_fadein)
            {
                _t -= Time.deltaTime / delay;
                if (_t < 0)
                {
                    enabled = false;
                    return;
                }
            }
            else
            {
                _t += Time.deltaTime / delay;
                if (_t > 1)
                {
                    enabled = false;
                    _load.CurtainClosed();
                    return;
                }
            }
            _image.color = new Color(0f, 0f, 0f, _t);
        }


        public void DrawCurtains(LoadScene loadScene)
        {
            _t = 0;
            _image.color = new Color(0f, 0f, 0f, 0f);
            _fadein = false;
            enabled = true;
            _load = loadScene;
        }
    }
}
