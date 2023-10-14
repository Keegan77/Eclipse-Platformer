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

    //public GameObject myObject;

    // transform? 

    Renderer myRenderer;

    MeshRenderer myMeshRenderer;

    private myMove myMove;

    private void myInteractionDay()
    { // here we define what properties the object will have during the day

        //myRenderer.material.shader = myDayShader;
       //. myRenderer.material.mainTexture = dayTexture;

        myMeshRenderer.material = myDayTexture;

    }
    private void myInteractionNight() 
    { // here we define what properties the object will have at night 

        //myRenderer.material.mainTexture = nightTexture;


        myMeshRenderer.material = myNightTexture;
        //myRenderer.material.shader = myNightShader;
    }

    private void OnEnable()
    {
        myMove.timeChange += myInteraction;
    }

    private void OnDisable()
    {
        myMove.timeChange -= myInteraction;
    }

    private void myInteraction()
    {
        if (myMove.currentTime/*myMove.getTime()*/)
            myInteractionNight();
        else
            myInteractionDay();


    }


    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        myMeshRenderer = GetComponent<MeshRenderer>();
    }


}

