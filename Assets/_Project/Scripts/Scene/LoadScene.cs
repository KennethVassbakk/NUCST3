// Author: John Hauge

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Scene
{
    public sealed class LoadScene : MonoBehaviour
    {
        private SceneTransition _transition;
        [FormerlySerializedAs("SceneNR")] public int sceneNr;

        private void Start()
        {
            _transition = FindObjectOfType<SceneTransition>();
        }

        private static void LoadTheScene(int sceneInt) => SceneManager.LoadScene(sceneInt);

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _transition.DrawCurtains(this);
            }
        }

        public void CurtainClosed()
        {
            LoadTheScene(sceneNr);
        }
    }
}
