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


    public Material myDayTexture, myNightTexture;
    private Rigidbody myRigidbody;
    private Rigidbody tempRigidbody;
    int frames;

    private stateChangeEffect stateShift;


    private Collider myCollider;

    public bool dayCollision;
    public bool nightCollision;

    private Renderer myRenderer;

    private MeshRenderer myMeshRenderer;

    private bool counting = false;





    private void myInteractionDay()
    { // here we define what properties the object will have during the day


        if (!dayCollision)
        {

            gameObject.GetComponent<Renderer>().enabled = false;

            myCollider.excludeLayers = 0;
            myRigidbody.excludeLayers = 0;
            myCollider.enabled = false;

        }
        else
        {


            gameObject.GetComponent<Renderer>().enabled = true;

            myCollider.enabled = true;
            myCollider.includeLayers = 0;
            myRigidbody.includeLayers = 0;

            myMeshRenderer.material = myDayTexture;
        }

    }
    private void myInteractionNight()
    { // here we define what properties the object will have at night 


        if (!nightCollision)
        {

            gameObject.GetComponent<Renderer>().enabled = false;

            myCollider.excludeLayers = 0;
            myRigidbody.excludeLayers = 0;
            myCollider.enabled = false;

        }
        else
        {

            gameObject.GetComponent<Renderer>().enabled = true;
            myMeshRenderer.material = myNightTexture;

            myCollider.enabled = true;
            myCollider.includeLayers = 0;
            myRigidbody.includeLayers = 0;
            return;
        }


    }

    private void myInteraction()
    {

        if (stateShift.timeOfDay)
            myInteractionNight();
        else
            myInteractionDay();

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


        if (myRigidbody == null)
        {
            myRigidbody = gameObject.AddComponent<Rigidbody>();

            myRigidbody.useGravity = false;

            myRigidbody.isKinematic = true;
        }

        myCollider.includeLayers = 7;
        stateShift.myShift += myInteraction;



        if (stateShift.timeOfDay)
            myInteractionNight();
        else
            myInteractionDay();

    }

    //private void OnTriggerEnter(Collider other)
    //{   // if we are entered by the object we want to see
    //    if (other.gameObject.TryGetComponent<stateChangeEffect>(out stateChangeEffect changer))
    //    {
    //        if (stateShift.timeOfDay)
    //            myInteractionDay();
    //        else
    //            myInteractionNight();

    //        counting = false;
    //    }
    //    return;
    //}

}




