using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    private CharacterInputController _input;

    [SerializeField] private bool rotateTowardsMouse = false;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody _rb;
    private Camera _cam;
    private Plane _intersectPlane;
    
    private void Awake()
    {
        _input = GetComponent<CharacterInputController>();
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;

        _intersectPlane = new Plane(Vector3.up, Vector3.zero);
    }
    
    private void FixedUpdate()
    {
        // Character Movement
       var moveVector = MoveTowardsVector(_input.inputVector);
        
        // Look towards mouse, or towards our direction
        if (rotateTowardsMouse)
            RotateToMouse(_input.mousePosition);
        else
            RotateToDirection(moveVector);
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
    
    private void RotateToDirection(Vector3 moveVector)
    {
        // If we're moving slowly, dont bother
        if (moveVector.magnitude < 0.1f)
            return;

        var rotation = Quaternion.LookRotation(moveVector);
        _rb.rotation = Quaternion.RotateTowards(_rb.rotation, rotation, moveSpeed * Mathf.PI);
    }

    private Vector3 MoveTowardsVector(Vector3 moveVector)
    {
        // If we dont have  any magnitude to our movement, dont run.
        if (moveVector.magnitude < 0.1f)
            return Vector3.zero;
        
        moveVector = Quaternion.Euler(0f, _cam.transform.eulerAngles.y, 0f) * moveVector.normalized;
        _rb.MovePosition(_rb.position + moveVector * (moveSpeed * Time.fixedDeltaTime));
        
        // Return our direction
        return moveVector;
    }
}
