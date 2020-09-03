// Author: Kenneth Vassbakk

using UnityEngine;

namespace Character
{
    public class CharacterMotor : MonoBehaviour
    {
        private const float GRAVITY_VALUE = -9.81f;

        //  Used for handling slope stuttering
        private const float SLOPE_FORCE = 5;

        private const float SLOPE_FORCE_RAY_LENGTH = 0.5f;

        //private Rigidbody _rb;
        private Camera _cam;
        private CharacterAnimation _characterAnimation;

        private CharacterController _characterController;
        private CharacterInputController _input;
        private Plane _intersectPlane;

        [SerializeField] private Vector3 _moveVector;
        [SerializeField] private bool _playerGrounded;
        [SerializeField] private bool _groundSlope;

        [SerializeField] private float moveSpeed = 5f;

        private void OnEnable()
        {
            _input = GetComponent<CharacterInputController>();
            _characterAnimation = GetComponent<CharacterAnimation>();
            _moveVector = Vector3.zero;
            _characterController = GetComponent<CharacterController>();
            _cam = Camera.main;

            // Used for mouse rotation
            _intersectPlane = new Plane(Vector3.up, transform.position);
        }

        private void Update()
        {
            _characterAnimation.AnimateMovement(transform, _input.inputVector);
            
            _playerGrounded = _characterController.isGrounded;
            
            if(_playerGrounded)
                _moveVector = MoveTowardsVector(_input.inputVector);
            
            //  Check Slope if we're grounded
            if(_moveVector.magnitude > 0.1 && _playerGrounded)
                SlopeCheck();
            
            // If we're grounded and falling, set velocity to 0
            if (_playerGrounded && _moveVector.y < 0)
                _moveVector.y = 0f;
            
            // Handle character Rotation
            RotateToMouse(_input.mousePosition);

            /*Look towards mouse, or towards our direction
            if (rotateTowardsMouse)
                RotateToMouse(_input.mousePosition);
            else
                RotateToDirection(moveVector);
            */
            
            // Gravity
            _moveVector.y += Physics.gravity.y * Time.fixedDeltaTime;
            
            // If we're on a slope, magnify to avoid stutter.
            if (_groundSlope)
                _moveVector.y += Physics.gravity.y * SLOPE_FORCE;
            
            // Finally we need to update our Intersect plane, to match our position
            _intersectPlane = new Plane(Vector3.up, transform.position);
        }

        private void FixedUpdate()
        {
            // Moving the controller should only be done ONCE, and during fixed update
            _characterController.Move(_moveVector * Time.fixedDeltaTime);
        }

        private void SlopeCheck()
        {
            //Physics.OverlapSphere(transform.position, 0.3f)
            if (Physics.Raycast(transform.position, Vector3.down, out var hit,
                _characterController.height / 2 * SLOPE_FORCE_RAY_LENGTH))
            {
                //_playerGrounded = true;
                _groundSlope = hit.normal != Vector3.up;
            }
            else
            {
                _groundSlope = false;
            }
        }

        private void RotateToMouse(Vector2 inputMousePosition)
        {
            // TODO: Rotation Joints.
            // Currently rotation is forced, the character glides on the ground to rotate towards the mouse.
            // A more natural rotation would be head -> torso -> feet.

            // If the magnitude is low, dont run
            if (inputMousePosition.magnitude < 0.1f)
                return;

            var ray = _cam.ScreenPointToRay(inputMousePosition);

            // If the ray doesnt hit anything, we can just return.
            if (!_intersectPlane.Raycast(ray, out var distance)) return;

            var direction = ray.GetPoint(distance) - transform.position;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //_rb.rotation = Quaternion.Euler(0, angle, 0);
            //transform.rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, angle, 0), Time.fixedDeltaTime * 800);
        }


        /*
        // Keeping this in, so we have it in case we need it for something later.
        private void RotateToDirection(Vector3 moveVector)
        {
            // If we're moving slowly, dont bother
            if (moveVector.magnitude < 0.1f)
            {
                return;
            }

            var rotation = Quaternion.LookRotation(moveVector);
            _rb.rotation = Quaternion.RotateTowards(_rb.rotation, rotation, moveSpeed * Mathf.PI);
        }*/

        /// <summary>
        /// Move the character toward the input vector
        /// in this case, the WASD key relationship.
        /// </summary>
        /// <param name="inputVector"></param>
        /// <returns></returns>
        private Vector3 MoveTowardsVector(Vector3 inputVector)
        {
            // If we dont have  any magnitude to our movement, dont run.
            if (inputVector.magnitude < 0.1f) 
                return new Vector3(0f, _moveVector.y, 0f);

            inputVector = Quaternion.Euler(0f, _cam.transform.eulerAngles.y, 0f) * inputVector.normalized ;
            //_rb.MovePosition(_rb.position + moveVector * (moveSpeed * Time.fixedDeltaTime));
            //_characterController.Move(moveVector * (Time.deltaTime * moveSpeed));
            return new Vector3(inputVector.x * moveSpeed, _moveVector.y, inputVector.z * moveSpeed) ;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            var position = transform.position;
            var raydistance = position - new Vector3(0f, _characterController.height / 2 * SLOPE_FORCE_RAY_LENGTH, 0f);
            Debug.DrawLine(position, raydistance, Color.yellow, Time.deltaTime);
        }
#endif
    }
}