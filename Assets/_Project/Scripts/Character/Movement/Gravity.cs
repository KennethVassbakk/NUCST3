// Author: Kenneth Vassbakk

using UnityEngine;

namespace Character.Movement
{
    public class Gravity : MonoBehaviour, IMovementModifier
    {
        // References
        private CharacterController _characterController;
        private MovementHandler _movementHandler;

        [Header("Settings")] 
        
        // This is added to smooth out stairstepping on uneven surfaces.
        [SerializeField] private float groundPullForce = 10f;

        private readonly float _gravityMagnitude = Physics.gravity.y;
        private bool _wasGroundedLastFrame;
        
        public Vector3 Value { get; private set; }
        
        // Add & Remove Modifier
        private void OnEnable() => _movementHandler.AddModifier(this);
        private void OnDisable() => _movementHandler.RemoveModifier(this);

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _movementHandler = GetComponent<MovementHandler>();
        }

        private void FixedUpdate() => ProcessGravity();

        private void ProcessGravity()
        {
            if (_characterController.isGrounded)
                Value = new Vector3(Value.x, -groundPullForce, Value.z);
            else if (_wasGroundedLastFrame)
                Value = Vector3.zero;
            else
                Value += new Vector3(0f, _gravityMagnitude * Time.deltaTime, 0f);

            _wasGroundedLastFrame = _characterController.isGrounded;
        }
    }
}