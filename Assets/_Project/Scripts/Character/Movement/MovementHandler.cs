// Author: Kenneth Vassbakk

using System.Collections.Generic;
using System.Linq;
using Character.Animation;
using UnityEngine;

namespace Character.Movement
{
    public class MovementHandler : MonoBehaviour
    {
        // Could move this out to its own class but..
        private List<IMovementModifier> _modifiers = new List<IMovementModifier>();
        
        // References
        private CharacterController _characterController;
        private AnimationHandler _animationHandler;

        // Add and remove modifiers
        public void AddModifier(IMovementModifier modifier) => _modifiers.Add(modifier);
        public void RemoveModifier(IMovementModifier modifier) => _modifiers.Remove(modifier);

        private Vector3 _movementVector;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animationHandler = GetComponent<AnimationHandler>();
        }

        private void Update()
        {
            if (_characterController.isGrounded)
                _animationHandler.AnimateMovement(transform, _movementVector);
            else
                _animationHandler.AnimateMovement(transform, Vector3.zero);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _movementVector = _modifiers.Aggregate(Vector3.zero, (current, modifier) => current + modifier.Value);
            
            _characterController.Move(_movementVector * Time.fixedDeltaTime);
        }
    }
}