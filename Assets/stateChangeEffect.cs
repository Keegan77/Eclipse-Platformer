using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
//using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;


// this script attaches to the player object
//anchors?

// frensel effect!

// findgameobject 

public class stateChangeEffect : MonoBehaviour
{
    //private timeOfDay timeOfDay;
    public bool timeOfDay = true;
    public bool timerEnable = true;
    private MeshRenderer myMeshRenderer;
    public Material myDayTexture, myNightTexture;
    //public float offset;
    private Shader myShader;

    public delegate void MyDelegate();
    public MyDelegate myShift;

    //public GameObject dayProcess;
    //public GameObject nightProcess;


    //private GameObject myGameObject;
    private Collider myCollider;


    // # of frames we expand for
    public int myTimer = 1;
    private int tempTimer;

    private float myTempIncrement;
    private float myIncrement;
    //private float myColorIncrement;

    private float myThingy = 1;
    private float myThingyIncrement = 0.1f;

    public float expandRate = 1.5f;
    Vector3 myVector3 = Vector3.one;


    private Input myInput = null; 


    // Start is called before the first frame update
    void Start()
    {
        //timeOfDay = GetComponent<timeOfDay>();
        myMeshRenderer = GetComponent<MeshRenderer>();
        myShader = GetComponent<Shader>();
        myCollider = GetComponent<Collider>();

        //dayProcess = find;

        // expand rate applied here
        //myVector3 = myVector3 * expandRate;

        // disable effect kinda
        transform.localScale = Vector3.zero;
        myCollider.includeLayers = 7;
        transform.position += (Vector3.up * .25f);//offset);
        Debug.Log(transform.position);

        if (!timerEnable)
            myTimer = 1;


        myCollider.isTrigger = true;

        myIncrement = (0.5f / ((float)myTimer));

        tempTimer = myTimer + 1;
        myThingyIncrement = (10 / (float)myTimer);

    }

    private void OnDestroy()
    {   // remove event delegate

        myShift = null;

    }

    private void Awake()
    {
            myInput = new Input();
    }

    private void OnEnable()
    {
        myInput.Enable();

        myInput.Player.SwitchTime.performed += myButtonPress;

    }
    private void OnDisable()
    {
        myInput.Disable();
    }

    private void myButtonPress(InputAction.CallbackContext  myContext) 
    {
        myInteraction();
        myShift?.Invoke();

    }
    private void myInteraction()
    {
        //slow mow!
        // enable sphere
        transform.localScale = Vector3.one;

        // reset timer
        //if (timerEnable)
        //{
            tempTimer = 1;
            myThingy = 1f + myThingyIncrement;
        //}


        if (timeOfDay)
        {
            myMeshRenderer.material = myNightTexture;
            timeOfDay = !timeOfDay;
            //nightProcess.SetActive(false);
            //dayProcess.SetActive(true);
        }
        else
        {

            myMeshRenderer.material = myDayTexture;
            timeOfDay = !timeOfDay;
            //nightProcess.SetActive(true);
            //dayProcess.SetActive(false);
        }


    }
   
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("here");

        if (tempTimer <= myTimer)
        {

            Time.timeScale = Mathf.Log10(myThingy);
            // increment timer
            tempTimer += 1;
            myThingy += myThingyIncrement;
            // EXPAND
            transform.localScale += myVector3;
        }
        else // this is happening every frame :/
        { // timer is up 
            Time.timeScale = 1.0f;
            transform.localScale = Vector3.zero;
        }


    }
}
