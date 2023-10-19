using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //This script is for common variables that every concrete state requires for calculations,
    //and common methods that they need, components, etc.
    //todo: refactor player movement vars into scriptable objects.
    //this script also handles input

    [HideInInspector] public Input input = null;

    [Header("Ground Movement Variables")]
    public float maxSpeed;
    [HideInInspector]public float speedTarget;

    public float turningSpeed;

    public float deccelSpeed;
    public float accelSpeed;

    public float jumpAmount;

    [HideInInspector] public float currentSpeed;
    [Header("Airborne Movement Variables")]
    public float jumpCutMultiplier;

    //components
    [Header("Component Refs")]

    public Rigidbody rb;
    public Transform cam;
    public Animator animator;
    public Transform groundCheck, playerRot;
    public LayerMask whatIsGround;

    //public Animator animator;

    //groundCollDebug
    [Header("Debug")]
    [Range(0, 10)] public float groundCollX;
    [Range(0, 40)] public float groundCollY;
    [Range(0, 10)] public float groundCollZ;

    //state machine
    public PlayerStateMachine stateMachine;
    public PlayerGroundMovementState movementState;
    public PlayerAirborneState airborneState;
    public PlayerIdleState idleState;

    //getters
    public Vector3 movedirection { get; private set; }
    public float targetAngle {get; private set;}


    [HideInInspector] public Vector3 direction;

    private void Awake()
    {
        input = new Input();
        //state refs
        stateMachine = new PlayerStateMachine();
        movementState = new PlayerGroundMovementState(this, stateMachine);
        airborneState = new PlayerAirborneState(this, stateMachine);
        idleState = new PlayerIdleState(this, stateMachine);
    }

    private void Start()
    {
        speedTarget = maxSpeed;
        stateMachine.Init(idleState);
    }

    private void Update()
    {
        stateMachine.currentPlayerState.StateUpdate();
        CheckGround();
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        stateMachine.SwitchState(airborneState);
    }

    private void FixedUpdate()
    {
        stateMachine.currentPlayerState.StateFixedUpdate();
        targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y; // get target angle here so all states can inheret them from player

        movedirection = Quaternion.Euler(direction.x, targetAngle, direction.z) * Vector3.forward;
        speedTarget = Mathf.Clamp(speedTarget, -maxSpeed, maxSpeed);
    }

    public bool CheckGround()
    {
        return Physics.CheckBox(groundCheck.position,
                                new Vector3(transform.localScale.x / groundCollX,
                                transform.localScale.y / groundCollY,
                                transform.localScale.z / groundCollZ),
                                playerRot.rotation,
                                whatIsGround);
        //we divide scale x and z by 2 because physics.checkbox wants half of a cube,
        //and then upscales it by 2
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position,
                            new Vector3(transform.localScale.x / groundCollX,
                                        transform.localScale.y / groundCollY,
                                        transform.localScale.z / groundCollZ) * 2);
    }


    public void AnimationTriggerEvent(string anim)
    {
        animator.SetBool(anim, true);
    }
    public void AnimationFinishedEvent(string anim)
    {
        animator.SetBool(anim, false);
    }


    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += GetMoveInput;
        input.Player.Movement.canceled += OnMoveInputCancelled;
        input.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= GetMoveInput;
        input.Player.Movement.canceled -= OnMoveInputCancelled;
        input.Player.Jump.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (stateMachine.currentPlayerState == movementState || stateMachine.currentPlayerState == idleState)
        {
            Jump();
        }
    }
    private void GetMoveInput(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>().normalized;
    }
    private void OnMoveInputCancelled(InputAction.CallbackContext ctx)
    {
        direction = Vector2.zero;
    }

}
