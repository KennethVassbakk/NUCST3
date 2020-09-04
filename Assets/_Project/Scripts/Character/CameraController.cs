// Author: Kenneth Vassbakk

using Cinemachine;
using UnityEngine;

namespace Character.OldController
{
    public class CameraController : MonoBehaviour
    {
        private float _currentDistance = 1f;
        private CinemachineFramingTransposer _framingTransposer;
        public float angleMax = 65f;

        public float angleMin = 35f;
        public float distanceMax = 15f;
        public float distanceMin = 10f;
        public static CameraController Instance { get; set; }

        public CinemachineVirtualCamera Camera { get; protected set; }

        // Initialize
        private void Awake()
        {
            Instance = this;
            Camera = GetComponent<CinemachineVirtualCamera>();
            _framingTransposer = Camera.GetCinemachineComponent<CinemachineFramingTransposer>();

            Zoom(0); // Init the zoom as well
        }

        /// <summary>
        /// Currently only used by itself.
        /// May be used if we add zoom functionality to the game - if so, make public and run from CharacterInputController, or a CameraInputController
        /// </summary>
        /// <param name="distance"></param>
        private void Zoom(float distance)
        {
            _currentDistance = Mathf.Clamp01(_currentDistance + distance); // Clamp the value between 0 and 1

            var rotation = transform.rotation.eulerAngles; 
            rotation.x = Mathf.LerpAngle(angleMin, angleMax, _currentDistance); // Lerp between min max, and our currentDistance
            transform.rotation = Quaternion.Euler(rotation);

            _framingTransposer.m_CameraDistance = Mathf.Lerp(distanceMin, distanceMax, _currentDistance); // Lerp our distance.
        }
    }
}