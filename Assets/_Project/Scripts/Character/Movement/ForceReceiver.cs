// Author: Kenneth Vassbakk

using UnityEngine;

namespace Character.Movement
{
    public class ForceReceiver : MonoBehaviour, IMovementModifier
    {
        [Header("Settings")] 
        [SerializeField] private float drag = 5f;
        [SerializeField] private float mass = 1f;
        
        // References
        private MovementHandler _movementHandler;
        private CharacterController _characterController;

        private bool _wasGroundedLastFrame;
        public Vector3 Value { get; private set; }

        // Add & Remove modifier
        private void OnEnable() => _movementHandler.AddModifier(this);
        private void OnDisable() => _movementHandler.RemoveModifier(this);

        private void Awake()
        {
            _movementHandler = GetComponent<MovementHandler>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!_wasGroundedLastFrame && _characterController.isGrounded)
                Value = new Vector3(Value.x, 0f, Value.z);

            _wasGroundedLastFrame = _characterController.isGrounded;

            if (Value.magnitude < 0.1f)
                Value = Vector3.zero;

            Value = Vector3.Lerp(Value, Vector3.zero, drag * Time.deltaTime);
        }

        public void AddForce(Vector3 force) => Value += force / mass;
    }
}