// Author: Kenneth Vassbakk

using UnityEngine;

namespace Character
{
    public class CharacterMotor : MonoBehaviour
    {
        private CharacterInputController _input;
        
        [SerializeField] private float moveSpeed = 5f;
        private Rigidbody _rb;
        private Camera _cam;
        private Plane _intersectPlane;
        private CharacterAnimation _characterAnimation;
        
        private void OnEnable()
        {
            _input = GetComponent<CharacterInputController>();
            _characterAnimation = GetComponent<CharacterAnimation>();
            _rb = GetComponent<Rigidbody>();
            _cam = Camera.main;

            // Used for mouse rotation
            _intersectPlane = new Plane(Vector3.up, Vector3.zero);
            
        }
    
        private void FixedUpdate()
        {
            // Character Movement
            MoveTowardsVector(_input.inputVector);
            _characterAnimation.AnimateMovement(_rb, _input.inputVector);
            
            // Handle character Rotation
            RotateToMouse(_input.mousePosition);
            
            /*Look towards mouse, or towards our direction
            if (rotateTowardsMouse)
                RotateToMouse(_input.mousePosition);
            else
                RotateToDirection(moveVector);
            */
            
        }

        private void RotateToMouse(Vector2 inputMousePosition)
        {
            // If the magnitude is low, dont run
            if (inputMousePosition.magnitude < 0.1f)
                return;

            var ray = _cam.ScreenPointToRay(inputMousePosition);
        
            // If the ray doesnt hit anything, we can just return.
            if (!_intersectPlane.Raycast(ray, out var distance)) return;
        
            var direction = ray.GetPoint(distance) - _rb.position;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _rb.rotation = Quaternion.Euler(0, angle, 0);
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
        
        // The return is not currently used, but we might use it sometime.
        // ReSharper disable once UnusedMethodReturnValue.Local
        private Vector3 MoveTowardsVector(Vector3 moveVector)
        {
            var moveVectorNormalized = moveVector.normalized;
            // If we dont have  any magnitude to our movement, dont run.
            if (moveVector.magnitude < 0.1f)
            {
                return Vector3.zero;
            }
            moveVector = Quaternion.Euler(0f, _cam.transform.eulerAngles.y, 0f) * moveVectorNormalized;
            _rb.MovePosition(_rb.position + moveVector * (moveSpeed * Time.fixedDeltaTime));
            
            return moveVector;
        }


    }
}
