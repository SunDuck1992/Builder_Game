using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(HashPlayerAnimations))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private const string VerticalDirection = "Vertical";
    private const string HorizontalDirection = "Horizontal";

    private Rigidbody _rigidbody;
    private Animator _animator;
    private HashPlayerAnimations _hashPlayer;
    private Vector3 movement;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _hashPlayer = GetComponent<HashPlayerAnimations>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float xDirection = Input.GetAxisRaw(HorizontalDirection);
        float zDirection = Input.GetAxisRaw(VerticalDirection);

        movement = new Vector3(xDirection, 0, zDirection);

        _rigidbody.MovePosition(_rigidbody.position + movement * _speed * Time.fixedDeltaTime);

        if (xDirection != 0 || zDirection != 0)
        {
            _animator.SetBool(_hashPlayer.Walk, true);
        }
        else
        {
            _animator.SetBool(_hashPlayer.Walk, false);
        }
    }

    private void Rotate()
    {
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed));
        }
    }
}
