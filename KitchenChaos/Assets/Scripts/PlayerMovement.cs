using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 _inputVector = Vector2.zero;
    private Vector3 _moveDir = Vector3.zero;

    public bool _isWalking;

    [SerializeField]
    private float _speedMultiplier = 5f;

    [SerializeField]
    private float _rotateSpeed = 10f;

    public Action OnStartWalking = delegate { };
    public Action OnStopWalking = delegate { };

    public void Update()
    {
        _inputVector = Vector2.zero;
        _moveDir = Vector3.zero;

        if (Input.GetKey("w"))
            _inputVector.y = 1;
        if (Input.GetKey("s"))
            _inputVector.y = -1;
        if (Input.GetKey("a"))
            _inputVector.x = -1;
        if (Input.GetKey("d"))
            _inputVector.x = 1;

        _inputVector = _inputVector.normalized;
        _moveDir.x = _inputVector.x;
        _moveDir.z = _inputVector.y;

        if (_inputVector != Vector2.zero)
        {
            if (!_isWalking)
            {
                _isWalking = true;
                OnStartWalking.Invoke();
            }

            transform.position += _moveDir * (_speedMultiplier * Time.deltaTime);

            //Changing blue axis of the object. Because object is rotated in 180 degrees, i use minus moveDir (-moveDir)
            transform.forward = Vector3.Slerp(transform.forward, -_moveDir, _rotateSpeed * Time.deltaTime);
        }
        else
        {
            if (_isWalking)
            {
                _isWalking = false;
                OnStopWalking.Invoke();
            }
        }
    }
}