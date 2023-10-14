using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class objectEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    //GameObject myObject;

    //GameObject.renderer.material.shader;
    public GameObject myNightObject;
    public GameObject myDayObject;
    public GameObject genericObject;

    public MeshRenderer myDayRenderer;
    public MeshRenderer myNightRenderer;

    public Texture dayTexture, nightTexture;

    public Shader myDayShader;
    public Shader myNightShader;

    private myMove myMove;

    //myMove character;
    //Character.currentTime = true;

    //bool time = character.currentTime;

    private void Start()
    {
        //genericObject.GetComponent(typeof(Renderer)) as ;

        //myDayRenderer.material.SetTexture("this",number);

    }

    private void myInteraction() 
    {
        if (myMove.getTime()) {
            // day to night
            myInteractionNight();
        }
        //night to day
        myInteractionDay();
    }

    private void myInteractionDay() { 
    
        //genericObject.
    
    }// sets daytime variables
    private void myInteractionNight() { }// sets nighttime variables

    private void OnEnable()
    {
        //timeChange += myInteraction;

        myMove.timeChange += myInteraction;
    }

    private void OnDisable()
    {
        //timeChange -= myInteraction;
    }
}
