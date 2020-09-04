// Author: John Hauge

using Systems.Cooldown;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public sealed class LoadScene : MonoBehaviour
    {
        private SceneTransition _transition;
        private ICooldown _cooldown;
        [SerializeField] public int sceneNr;

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
