using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Day2Night : MonoBehaviour
{

    //             Read me 
    // ---------------------------------------------------------//


    // myMove is the class where the delegate event exists. 
    // we add function to the delegate to trigger them on the time change
    // we keep track of the time with a boolean; true = state is day
    //                                           false = state is night
    // when we move between the two we need to set a day and night time value
    // for whatever we want to change 


    //--------------------------------------------------------//


    //public Shader myDayShader, myNightShader;

    //public Texture dayTexture, nightTexture;

    public Material myDayTexture, myNightTexture;
    private Rigidbody myRigidbody;
    int frames;
    //private Time myTime;

    public stateChangeEffect stateShift;

    //public GameObject myObject;

    public Collider myCollider;

    public bool dayCollision;
    public bool nightCollision;

    // transform? 

    //Renderer myRenderer;

    private MeshRenderer myMeshRenderer;
    //private bool wait = false;
    private bool counting = false;

    private myMove myMove;
    

    private void myInteractionDay()
    { // here we define what properties the object will have during the day


        if (!dayCollision)
        {
            myCollider.excludeLayers = 0;
        }
        else
        {
            myCollider.includeLayers = 0;
        }

        myMeshRenderer.material = myDayTexture;

        

    }
    private void myInteractionNight()
    { // here we define what properties the object will have at night 


        if (!nightCollision)
        {
            myCollider.excludeLayers = 0;

        }
        else
        {
            myCollider.includeLayers = 0;

        }

        myMeshRenderer.material = myNightTexture;

    }

    private void myInteraction()
    {

        if (counting == true) {
            // if the player pressed the button before the timer went off 
            if (myMove.currentTime)
                myInteractionNight();
            else
                myInteractionDay();
        }

        // start counting 
        counting = true;
        // get the current frame for timer reference
        frames = Time.frameCount;

    }

    private void OnDestroy() 
    {
        stateShift.myShift -= myInteraction;
    }


    private void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();

        myCollider = GetComponent<Collider>();

        if(myCollider == null)
            myCollider = gameObject.AddComponent<Collider>();

        myRigidbody = gameObject.AddComponent<Rigidbody>();

        myRigidbody.useGravity = false;


        myCollider.includeLayers = 7;
        stateShift.myShift += myInteraction;

        
    }

    private void OnTriggerEnter(Collider other)
    {   // if we are entered by the object we want to see
        Debug.Log(gameObject.name);
        if (other.gameObject.TryGetComponent<stateChangeEffect>( out stateChangeEffect changer)) {
            if (myMove.currentTime)
                myInteractionNight();
            else
                myInteractionDay();

            counting = false;
        }
        //wait = true; // we found player effect. flip
        return;
    }

    void Update() {




        //if (wait /*|| myTime*/) // we are checking for player effect
        //{
        //    if (myMove.currentTime)
        //        myInteractionNight();
        //    else
        //        myInteractionDay();
        if (counting && (Time.frameCount > (frames + stateShift.myTimer))) {
            // if the invoke has been called
            //     AND  we have not hit the effect
            //     AND  the timer has gone off
            counting = false;

            if (myMove.currentTime)
                myInteractionNight();
            else
                myInteractionDay();

        }

    }



}

