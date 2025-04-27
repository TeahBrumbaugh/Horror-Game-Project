using System.Collections;
 using System.Collections.Generic;
 using Unity.VisualScripting;
 using UnityEngine;
 
 public class PlayerMovement : MonoBehaviour
 {
     [SerializeField] private float _moveSpeed = 7f;
     [SerializeField] private GameInput _gameInput;
     [SerializeField] private Transform _cameraTransform;
     [SerializeField] private float _maxSpeed = 3f;
 
     private Rigidbody _rb;
     private Vector2 _moveInput;
 
     private void Start() {
         _rb = transform.GetChild(0).GetComponent<Rigidbody>();
         if (_rb != null) {
             Debug.Log("Found Rigidbody on child!");
         }
         else {
             Debug.Log("No Rigidbody found on child!");
         }
     }
 
     private void FixedUpdate() {
 
         Vector3 moveDir = _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
         moveDir.y = 0f;
 
         _rb.AddForce(moveDir.normalized * _moveSpeed, ForceMode.VelocityChange);
         _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, _maxSpeed);
     }
 
     public void Update() {
         _moveInput = _gameInput.GetMovementVector();
     }
 }