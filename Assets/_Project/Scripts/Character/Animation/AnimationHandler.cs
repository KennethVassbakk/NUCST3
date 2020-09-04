// Author: Kenneth Vassbakk

using UnityEngine;
    
namespace Character.Animation
{
    public class AnimationHandler : MonoBehaviour
    {
        // References
        private Animator _animator;
        
        // Animator Settings
        private int _aMovementSpeed;
        private int _aHorizontal;
        private int _aVertical;
        
        // Store current values
        private float _aCurrentMovementSpeed;
        private float _aCurrentHorizontal;
        private float _aCurrentVertical;
        
        // Blend speed
        private float _dampTime = 0.08f;
        
#if UNITY_EDITOR
        private Vector3 _debugVector;
#endif
        
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
        /// <param name="characterTransform">Character transform</param>
        /// <param name="moveVector">Vector3 MovementDirection (input)</param>
        public void AnimateMovement(Transform characterTransform, Vector3 moveVector)
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
                _animator.SetFloat(_aHorizontal, (_aCurrentHorizontal > 0.05f) ? Mathf.Lerp(_aCurrentHorizontal, 0f, _dampTime) : 0f);
                _animator.SetFloat(_aVertical, (_aCurrentVertical > 0.05f) ? Mathf.Lerp(_aCurrentVertical, 0f, _dampTime) : 0f);
                return;
            }
            
            var position = characterTransform.position;
            
            // Point we're moving towards relative to the player location
            moveVector = new Vector3(moveVector.x, position.y, moveVector.z) + new Vector3(position.x, 0f, position.z);
            var moveToward = moveVector - transform.position;

            moveToward = moveToward.normalized;
            
            var relativePosition = characterTransform.InverseTransformDirection(moveToward);
            
            // We currently dont need to know the relative Angle, but here's the method for it.
            // var relativeAngle = Vector3.Angle(_rb.transform.forward, moveToward);
            
            // Set Animation Settings
            _animator.SetFloat(_aMovementSpeed, moveVectorNormalized.magnitude);
            
            // Lerp animations
            _animator.SetFloat(_aHorizontal, relativePosition.x, _dampTime, Time.deltaTime);
            _animator.SetFloat(_aVertical, relativePosition.z, _dampTime, Time.deltaTime);
            
            // Used for the unity Editor
#if UNITY_EDITOR
            _debugVector = moveVector;
#endif
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_debugVector, 0.25f);
            Gizmos.color = Color.white;
        }
#endif
    }
}