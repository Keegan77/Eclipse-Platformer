using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Day2Night_first : MonoBehaviour
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
    private stateChangeEffect myStateChanger;

    private Rigidbody myRigidbody;

    //public GameObject myObject;

    public Collider myCollider;

    public bool dayCollision;
    public bool nightCollision;

    //public delegate void changeTime();
    //public static event changeTime timeChange;

    public static bool currentTime = true;

    // transform? 

    //Renderer myRenderer;

    private MeshRenderer myMeshRenderer;

    //private myMove myMove;

    private void myInteractionDay()
    { // here we define what properties the object will have during the day

        //myRenderer.material.shader = myDayShader;
        //myRenderer.material.mainTexture = dayTexture;
        //myObject.SetActive(true);
        if (dayCollision)
        {
            myCollider.enabled = true;
        }else 
            myCollider.enabled = false;

        myMeshRenderer.material = myDayTexture;
        

    }
    private void myInteractionNight() 
    { // here we define what properties the object will have at night 

        //myRenderer.material.mainTexture = nightTexture;

        //myObject.SetActive(false);
        if (nightCollision)
        {
            myCollider.enabled = true;
        }
        else
            myCollider.enabled = false;

        myMeshRenderer.material = myNightTexture;
        //myRenderer.material.shader = myNightShader;
    }

    //private void OnEnable()
    //{
    //    myMove.timeChange += myInteraction;
    //}

    //private void OnDisable()
    //{
    //    myMove.timeChange -= myInteraction;
    //}

    public void myInteraction()
    {
        if (currentTime)
            myInteractionNight();
        else
            myInteractionDay();
    }


    private void Start()
    {
        //myRenderer = GetComponent<Renderer>();
        myMeshRenderer = GetComponent<MeshRenderer>();
        //myObject = GetComponent<GameObject>();
        myCollider = GetComponent<Collider>();

        //timeChange += myInteraction;

        myRigidbody = GetComponent<Rigidbody>();

        if(myRigidbody != null)
        {

            myRigidbody = new Rigidbody();

        }

        //myRigidbody.
   
    }

    private void OnDestroy()
    {
        
    }


}

