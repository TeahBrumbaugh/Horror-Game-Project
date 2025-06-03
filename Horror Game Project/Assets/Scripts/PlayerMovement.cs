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

    [Header("Footstep Sound Settings")]
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private float _stepInterval = 0.4f;

    private Rigidbody _rb;
    private Vector2 _moveInput;
    private float _timeSinceLastStep = 0f;

    private void Start()
    {
        _rb = transform.GetChild(0).GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.Log("No Rigidbody found on child!");
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
        moveDir.y = 0f;
        _rb.AddForce(moveDir.normalized * _moveSpeed, ForceMode.VelocityChange);
        _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, _maxSpeed);

        _timeSinceLastStep += Time.fixedDeltaTime;

        bool isMoving = (_moveInput.x != 0f || _moveInput.y != 0f);
        if (isMoving && _timeSinceLastStep >= _stepInterval)
        {
            SoundManager.instance.PlaySoundEffectClip(walkSound, transform, 1f, 0.5f);
            _timeSinceLastStep = 0f;
        }
    }

    public void Update()
    {
        _moveInput = _gameInput.GetMovementVector();
    }
}
