// Author: Kenneth Vassbakk

using UnityEngine;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _animator;
        
        // Animator Settings
        private int _aMovementSpeed;
        private int _aHorizontal;
        private int _aVertical;
        
        // Store current values
        private float _aCurrentMovementSpeed;
        private float _aCurrentHorizontal;
        private float _aCurrentVertical;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            // Hash animator values to optimize performance
            _aMovementSpeed = Animator.StringToHash("MovementSpeed");
            _aHorizontal = Animator.StringToHash("Horizontal");
            _aVertical = Animator.StringToHash("Vertical");
            
        }
        
        /// <summary>
        /// Animate character based on its rigidbody and the movement vector.
        /// Rotation of the character is taken into accounnt.
        /// </summary>
        /// <param name="rb">Character Rigidbody</param>
        /// <param name="moveVector">Vector3 MovementDirection (input)</param>
        public void AnimateMovement(Rigidbody rb, Vector3 moveVector)
        {
            _aCurrentHorizontal = _animator.GetFloat(_aHorizontal);
            _aCurrentVertical = _animator.GetFloat(_aVertical);

            var moveVectorNormalized = moveVector.normalized;
            
            // If we're not really moving, we dont need to run much
            // Just make sure we're setting movementspeed to zero and
            // resetting the animation floats - to make it smoother.
            if (moveVector.magnitude < 0.1f)
            {
                _animator.SetFloat(_aMovementSpeed, 0f);
                
                // We also reset the horizontal and vertical of the animator; lerping to smooth transition
                _animator.SetFloat(_aHorizontal, (_aCurrentHorizontal > 0.05f) ? Mathf.Lerp(_aCurrentHorizontal, 0f, 0.25f) : 0f);
                _animator.SetFloat(_aVertical, (_aCurrentVertical > 0.05f) ? Mathf.Lerp(_aCurrentVertical, 0f, 0.25f) : 0f);
                return;
            }
            
            var position = rb.position;
            
            // Point we're moving towards relative to the player location
            moveVector = new Vector3(moveVector.x, position.y, moveVector.z) + position;
            var moveToward = moveVector - position;

            moveToward = moveToward.normalized;
            
            var relativePosition = rb.transform.InverseTransformDirection(moveToward);
            
            // We currently dont need to know the relative Angle, but here's the method for it.
            // var relativeAngle = Vector3.Angle(_rb.transform.forward, moveToward);
            
            // Set Animation Settings
            _animator.SetFloat(_aMovementSpeed, moveVectorNormalized.magnitude);
            _animator.SetFloat(_aHorizontal, Mathf.Lerp(_aCurrentHorizontal, relativePosition.x, 0.25f));
            _animator.SetFloat(_aVertical, Mathf.Lerp(_aCurrentVertical, relativePosition.z, 0.25f));

        }
    }
}