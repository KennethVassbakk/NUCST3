﻿// Author: Kenneth Vassbakk

using UnityEngine;

namespace Character
{
    public class CharacterMotor : MonoBehaviour
    {
        private const float GRAVITY_VALUE = -9.81f;

        //  Used for handling slope stuttering
        private const float SLOPE_FORCE = 3;

        private const float SLOPE_FORCE_RAY_LENGTH = 0.5f;

        //private Rigidbody _rb;
        private Camera _cam;
        private CharacterAnimation _characterAnimation;

        private CharacterController _characterController;
        private CharacterInputController _input;
        private Plane _intersectPlane;

        private Vector3 _moveVector;
        private bool _playerGrounded;

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
        }

        private void FixedUpdate()
        {
            // Character Movement - If the input vector is greater than 0.1
            _moveVector = MoveTowardsVector(_input.inputVector);
            
            _playerGrounded = _characterController.isGrounded;
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

            // Set velocity
            if (_moveVector.magnitude > 0.1 && OnSlope())
                _moveVector.y += GRAVITY_VALUE * SLOPE_FORCE;
            else
                _moveVector.y += GRAVITY_VALUE * Time.deltaTime;
            
            // Moving the controller should only be done ONCE.
            _characterController.Move(_moveVector * Time.fixedDeltaTime);
            
            // Finally we need to update our Intersect plane, to match our position
            _intersectPlane = new Plane(Vector3.up, transform.position);
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
            transform.rotation = Quaternion.Euler(0, angle, 0);
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

            inputVector = Quaternion.Euler(0f, _cam.transform.eulerAngles.y, 0f) * inputVector.normalized;
            //_rb.MovePosition(_rb.position + moveVector * (moveSpeed * Time.fixedDeltaTime));
            //_characterController.Move(moveVector * (Time.deltaTime * moveSpeed));
            return inputVector * moveSpeed;
        }
    
        /// <summary>
        /// Are we on a sloped terrain?
        /// </summary>
        /// <returns></returns>
        private bool OnSlope()
        {
            // If we're jumping, return false..
            // not implemented.

            if (Physics.Raycast(transform.position, Vector3.down, out var hit,
                _characterController.height / 2 * SLOPE_FORCE_RAY_LENGTH))
                return hit.normal != Vector3.up;
            return false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            var position = transform.position;
            var raydistance = position - new Vector3(0f, _characterController.height / 2 * SLOPE_FORCE_RAY_LENGTH, 0f);
            Debug.DrawLine(position, raydistance, Color.red, Time.deltaTime);
        }
#endif
    }
}