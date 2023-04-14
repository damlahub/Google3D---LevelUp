using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class RuntimeMovement : MonoBehaviour
{
    private Movement _input;
    private CharacterController _controller;
    [SerializeField] private float fraction=100f; // slowed down the speed
    private Animator _animator;
    private void Start()
    {
        _controller= GetComponent<CharacterController>();
        _input= GetComponent<Movement>();
        _animator= GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        _controller.Move(new Vector3((_input.moveVal.x*_input.moveSpeed)/fraction,0f,(_input.moveVal.y*_input.moveSpeed)/fraction));
        _animator.SetFloat("speed", Mathf.Abs(_controller.velocity.x ) + Mathf.Abs(_controller.velocity.z));
    }
}
