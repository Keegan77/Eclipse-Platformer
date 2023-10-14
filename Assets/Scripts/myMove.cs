using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class myMove : MonoBehaviour
{

    //public static event Action timeChange;

    // this delegate takes all the functions that we want to do once player presses time change
    // all objects that are going to be changed must add their change function to the delagate
    // example
    //playerObject.timeWasChanged += myFunction;
    //public delegate void dayTime();
    //public delegate void nightTime();
    //public static event dayTime timeChangeDay;
   // public static event nightTime timeChangeNight;

    public delegate void changeTime();
    public static event changeTime timeChange;


    public static bool currentTime = true; // true for day? idk 

    private CharacterController controller;
    private Vector3 velocity; 
    private float speed = 6.0f;
    private float jumpHeight = 20.0f;
    private float gravity = -5.0f;
    public bool playerOnGround;

    public void onTimeChangeDay() {

        speed = 6.0f;
        jumpHeight = 5.0f;
    
    }
    public void onTimeChangeNight()
    {
        speed = 3.0f;
        jumpHeight = 10.0f;

    }

    public bool getTime() {
        Console.Write(currentTime);
        return currentTime;
    }

    private void onTimeChange() { 
    
        if(currentTime)
        {
            // time is day we are switching to night
            onTimeChangeNight();
            return;
        }
        // time is night we are switching to day
        onTimeChangeDay();
    
    }
    private void updateBool() 
    { 
        currentTime = currentTime ? currentTime = false : true;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();

        timeChange += onTimeChange;

    }

    // Update is called once per frame
    void Update()
    {
        //playerOnGround = controller.isGrounded;
        //if (playerOnGround && velocity.y < 0)
        //{
        //    velocity.y = 0f;
        //}

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //controller.Move(move * Time.deltaTime * speed);

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        //// Changes the height position of the player..
        //if (Input.GetButtonDown("Jump") && playerOnGround)
        //{
        //    velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        //}

        if (Input.GetKeyDown(KeyCode.Q)) {

            timeChange?.Invoke();

            updateBool();
        }

        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
    }
}
