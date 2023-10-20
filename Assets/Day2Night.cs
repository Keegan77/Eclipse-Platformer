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
    private Rigidbody tempRigidbody;
    int frames;
    //private Time myTime;
    private stateChangeEffect stateShift;
    //public stateChangeEffect stateShift;

    //public GameObject myObject;

    private Collider myCollider;

    public bool dayCollision;
    public bool nightCollision;

    // transform? 

    //Renderer myRenderer;

    private Renderer myRenderer;

    private MeshRenderer myMeshRenderer;
    //private bool wait = false;
    private bool counting = false;


    //private myMove myMove;


    private void myInteractionDay()
    { // here we define what properties the object will have during the day


        if (!dayCollision)
        {
            //gameObject.SetActive(false);

            //return;
            gameObject.GetComponent<Renderer>().enabled = false;

            myCollider.excludeLayers = 0;
            myRigidbody.excludeLayers = 0;
            myCollider.enabled = false;
            //myMeshRenderer.material.color.a = 0;
            //myMeshRenderer.SetA


        }
        else
        {
            //gameObject.SetActive(true);

            //return;

            gameObject.GetComponent<Renderer>().enabled = true;

            myCollider.enabled = true;
            myCollider.includeLayers = 0;
            myRigidbody.includeLayers = 0;

            myMeshRenderer.material = myDayTexture;
        }

        //myMeshRenderer.material = myDayTexture;



    }
    private void myInteractionNight()
    { // here we define what properties the object will have at night 


        if (!nightCollision)
        {

            //gameObject.SetActive(false);
            gameObject.GetComponent<Renderer>().enabled = false;

            myCollider.excludeLayers = 0;
            myRigidbody.excludeLayers = 0;
            myCollider.enabled = false;

        }
        else
        {
            //gameObject.SetActive(true);

            //return;
            gameObject.GetComponent<Renderer>().enabled = true;
            myMeshRenderer.material = myNightTexture;

            myCollider.enabled = true;
            myCollider.includeLayers = 0;
            myRigidbody.includeLayers = 0;
            return;
        }

        //myMeshRenderer.material = myNightTexture;

    }

    private void myInteraction()
    {

        //if (!stateShift.timerEnable || counting == true) {
        // if the player pressed the button before the timer went off 
        if (stateShift.timeOfDay)//stateShift.timeOfDay)
            myInteractionNight();
        else
            myInteractionDay();
        //}

        // start counting 
        //counting = true;
        // get the current frame for timer reference
        //frames = Time.frameCount;

    }

    private void OnDestroy()
    {
        stateShift.myShift -= myInteraction;
    }


    private void Start()
    {

        stateShift = FindObjectOfType<stateChangeEffect>();


        myMeshRenderer = GetComponent<MeshRenderer>();
        myRenderer = GetComponent<Renderer>();

        myCollider = GetComponent<Collider>();

        if (myCollider == null)
            myCollider = gameObject.AddComponent<Collider>();

        myRigidbody = GetComponent<Rigidbody>();
        //tempRigidbody = new Rigidbody(myRigidbody);


        if (myRigidbody == null)
        {
            myRigidbody = gameObject.AddComponent<Rigidbody>();

            myRigidbody.useGravity = false;

            myRigidbody.isKinematic = true;
        }

        myCollider.includeLayers = 7;
        stateShift.myShift += myInteraction;//stateShift.myShift += myInteraction;

        //gameObject.GetComponent<Renderer>().enabled = false;

        if (stateShift.timeOfDay)//stateShift.timeOfDay)
            myInteractionNight();
        else
            myInteractionDay();

    }

    private void OnTriggerEnter(Collider other)
    {   // if we are entered by the object we want to see
        //Debug.Log(gameObject.name);
        if (other.gameObject.TryGetComponent<stateChangeEffect>(out stateChangeEffect changer))
        {
            if (stateShift.timeOfDay)//stateShift.timeOfDay)
                myInteractionDay();
            else
                myInteractionNight();

            counting = false;
        }
        //wait = true; // we found player effect. flip
        return;
    }

    void Update() { }
}




