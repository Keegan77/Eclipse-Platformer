using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Movement Variables")]
    [SerializeField] float maxSpeed;

    [SerializeField] float turningSpeed;

    [SerializeField] float deccelSpeed;
    [SerializeField] float accelSpeed;

    [SerializeField] float jumpAmount;

    [Header("Advanced Movement Variables")]
    [SerializeField] float playerGravity;
    [Tooltip("Increase gravity by this amount if the player is in first half of jump (before height climax)")]
    [Range(0, 1)]
    [SerializeField] float increaseGravityBy; //makes jump look snappier

    
    //priv components
    Rigidbody rb;
    ConstantForce extraGrav;
    [Header("Component Refs")]
    public Transform cam;
    public Animator animator;
    [SerializeField] Transform groundCheck, playerRot;
    public LayerMask whatIsGround;

    //public Animator animator;

    //priv vars
    float currentSpeed;
    bool isGrounded;

    //input vars
    float hoz;
    float vert;
    bool jumpInput;

    //groundCollDebug
    [Header("Debug")]
    [Range(0, 10)] public float groundCollX;
    [Range(0, 10)] public float groundCollY;
    [Range(0, 10)] public float groundCollZ;

    //state machine
    enum States
    {
        Idle,
        Run,
    }
    States currentState;

    Vector3 dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentState = States.Idle;
    }

    private void Update()
    {
        GetInput();
        CheckGround();
        Debug.Log(rb.velocity.y);


        dir = new Vector2(hoz, vert).normalized; // save direction via vector

        //UpdateAnimations();

        if (jumpInput)
        {
            Jump();
        }

    }
    private void GetInput()
    {
        hoz = Input.GetAxisRaw("Horizontal"); // get inputs
        vert = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
    }

    private void Jump()
    {
        if (isGrounded)
        {
            //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }
        
    }
    private void FixedUpdate()
    {
        GroundMovement();
        /*if (rb.velocity.y > 0 )
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * increaseGravityBy, rb.velocity.z);
        }*/
    }

/*    private void UpdateAnimations()
    {
        switch (currentState)
        {
            case States.Idle:
                {
                    animator.SetBool("still", true);
                    animator.SetBool("running", false);
                    break;
                }
            case States.Run:
                {
                    animator.SetBool("running", true);
                    animator.SetBool("still", false);
                    break;
                }
        }
    }*/

    private void GroundMovement()
    {
        Debug.Log(dir.magnitude);
        if (dir.magnitude > 0)
        {
            currentState = States.Run;
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * accelSpeed);

            float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + cam.eulerAngles.y; // gives the target angle based on input
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, targetAngle, 0)), Time.deltaTime * turningSpeed);
            Vector3 moveDir = Quaternion.Euler(dir.x, targetAngle, dir.z) * Vector3.forward;
            rb.velocity = new Vector3(moveDir.x * currentSpeed, rb.velocity.y, moveDir.z * currentSpeed); //affect movement

        }
        else
        {
            currentState = States.Idle;
            rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x, 0, Time.deltaTime * deccelSpeed),
                                                 rb.velocity.y,
                                      Mathf.Lerp(rb.velocity.z, 0, Time.deltaTime * deccelSpeed));
            currentSpeed = 0;
        }

    }

    void CheckGround()
    {
        isGrounded = Physics.CheckBox(groundCheck.position,
                                      new Vector3(transform.localScale.x / groundCollX, 
                                      transform.localScale.y / groundCollY, 
                                      transform.localScale.z / groundCollZ),
                                      playerRot.rotation, 
                                      whatIsGround); //we divide scale x and z by 2 because physics.checkbox wants half of a cube, and then upscales it by 2
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, 
                            new Vector3(transform.localScale.x / groundCollX,  
                                        transform.localScale.y / groundCollY, 
                                        transform.localScale.z / groundCollZ) * 2); //
    }


    private void UpdateAnimations()
    {
        switch (currentState)
        {
            case States.Idle:
                {
                    animator.SetBool("still", true);
                    animator.SetBool("running", false);
                    break;
                }
            case States.Run:
                {
                    animator.SetBool("running", true);
                    animator.SetBool("still", false);
                    break;
                }
        }
    }
}
