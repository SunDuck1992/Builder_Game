using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Joystick _joystick;

    private const string VerticalDirection = "Vertical";
    private const string HorizontalDirection = "Horizontal";
    private const string SpeedMultyPlie = "speedMulti";

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 movement;
   

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
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

        //xDirection = _joystick.Horizontal;
        //zDirection = _joystick.Vertical;

        movement = new Vector3(xDirection, 0, zDirection);
        _animator.SetFloat(SpeedMultyPlie, UpgradePlayer.Instance.MultiplieSpeed);

        //_rigidbody.MovePosition(_rigidbody.position + movement * _speed * UpgradePlayer.Instance.MultiplieSpeed * Time.fixedDeltaTime);

        _agent.speed = _speed * UpgradePlayer.Instance.MultiplieSpeed;
        _agent.velocity = movement.normalized * _agent.speed;
        if (xDirection != 0 || zDirection != 0)
        {
            _animator.SetBool(HashPlayerAnimations.Walk, true);
        }
        else
        {
            _animator.SetBool(HashPlayerAnimations.Walk, false);
        }
    }

    private void Rotate()
    {
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            //_rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed);
            //transform.forward = (transform.position +  movement) - transform.position;
        }
    }
}
