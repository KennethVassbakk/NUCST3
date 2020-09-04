// Author: Kenneth Vassbakk

using UnityEngine;

namespace Character.Controller
{
    public class MovementInputProcessor : MonoBehaviour, IMovementModifier
    {
        [Header("Settings")] 
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float acceleration = 30;

#if UNITY_EDITOR
        // ReSharper disable once NotAccessedField.Local
        [SerializeField] private Vector3 debugVector;
#endif
        // References
        private CharacterController _characterController;
        private MovementHandler _movementHandler;
        private Camera _camera;
        
        // Variables
        private Vector3 _moveVector;
        private Vector3 _previousInputDirection;
        private Vector2 _mousePosition;
        private bool _wasGroundedLastFrame;
        private Plane _intersectPlane;
        private float _currentSpeed;
        
        public Vector3 Value { get; private set; }

        private void Awake()
        {
            // Get all references.
            _characterController = GetComponent<CharacterController>();
            _movementHandler = GetComponent<MovementHandler>();
            _intersectPlane = new Plane(Vector3.up, transform.position);
            _moveVector = Vector3.zero;

            if (Camera.main != null) _camera = Camera.main;
        }

        // Add & Remove Modifier
        private void OnEnable() => _movementHandler.AddModifier(this);
        private void OnDisable() => _movementHandler.RemoveModifier(this);

        private void Update()
        {
            _wasGroundedLastFrame = _characterController.isGrounded;
            
            GetMovementInput();
            Move();
            RotateToMouse(_mousePosition);
            
            _intersectPlane = new Plane(Vector3.up, transform.position);
#if UNITY_EDITOR
            debugVector = Value;
#endif
        }
        
        private void GetMovementInput()
        {
            _mousePosition = Input.mousePosition;
            _previousInputDirection.x = Input.GetAxisRaw("Horizontal");
            _previousInputDirection.y = 0f;
            _previousInputDirection.z = Input.GetAxisRaw("Vertical");
            _previousInputDirection.Normalize();
        }
        
        private void Move()
        {
            var targetSpeed = movementSpeed * _previousInputDirection.magnitude;
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, acceleration * Time.deltaTime);

            if (_wasGroundedLastFrame)
                _moveVector = MoveTowardsVector(_previousInputDirection);
            
            Value = _moveVector * _currentSpeed;
        }
        
        private void RotateToMouse(Vector2 inputMousePosition)
        {
            // TODO: Rotation Joints.
            // Currently rotation is forced, the character glides on the ground to rotate towards the mouse.
            // A more natural rotation would be head -> torso -> feet.
            
            if (inputMousePosition.magnitude < 0.1f)
                return;

            var ray = _camera.ScreenPointToRay(inputMousePosition);

            // If the ray doesnt hit anything, we can just return.
            if (!_intersectPlane.Raycast(ray, out var distance)) return;

            var direction = ray.GetPoint(distance) - transform.position;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
  
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * 800);
        }
        
        private Vector3 MoveTowardsVector(Vector3 inputVector)
        {
            // If we dont have  any magnitude to our movement, dont run.
            if (inputVector.magnitude < 0.1f)
                return new Vector3(0f, _moveVector.y, 0f);
            
            inputVector = Quaternion.Euler(0f, _camera.transform.eulerAngles.y, 0f) * inputVector.normalized ;
            
            return new Vector3(inputVector.x, 0f, inputVector.z);
        }
    }
}