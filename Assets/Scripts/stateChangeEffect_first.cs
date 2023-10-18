using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;


// this script attaches to the player object
//anchors?
public class stateChangeEffect_first : MonoBehaviour
{
    private myMove myMove;
    private MeshRenderer myMeshRenderer;
    public Material myDayTexture, myNightTexture;
    private Shader myShader;
    private Rigidbody myRigidBody;

    //UnityEvent MyEvent;
    GameObject myGameObject;
    delegate void myDelegate();
    myDelegate stateChange;


    //private GameObject myGameObject;
    public Collider myCollider;


    // # of frames we expand for
    public int myTimer = 60;
    private int tempTimer;

    public bool enableSlowMo = true;
    //public bool triggerChangeOnCollision = false;

    private float myTempIncrement;
    private float myIncrement;
    private float myColorIncrement;

    private float myThingy = 0;
    private float TimeScaleLogIncrementPerFrame = 0;

    public float expandRate = 1.5f;
    Vector3 myVector3 = Vector3.one;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision detected");
        other.gameObject.GetComponent<EventTriggeringObjectScript>().MyDelegate();
        //if (collision.gameObject.TryGetComponent<EventTriggeringObjectScript>(out EventTriggeringObjectScript component)) {
        //    component.MyDelegate();
        //}
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision detected");
        myCollider.enabled = false;

        //timeChange?.Invoke();

        //Day2Night[] stateActors;

        //List<HingeJoint> hingeJoints = new List<HingeJoint>();

        //stateActors = other.gameObject.GetComponents(typeof(stateActors));

        //List<Day2Night> day2Nights = new List<Day2Night>();

        //other.gameObject.GetComponents(day2Nights);

        //foreach (Day2Night foo in day2Nights)
        //{
        //    foo.myInteraction();
        //}

        //other.gameObject.GetComponent<Day2Night>().myInteraction();

        //if (other.gameObject.TryGetComponent<Day2Night> (out Day2Night component)){

        //    Debug.Log("object has the component");
        //    component.myInteraction();
        //}


    }
    /*
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("collision exited");
        myCollider.enabled = true;
        //Collider.tri
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        myMove = GetComponent<myMove>();
        myMeshRenderer = GetComponent<MeshRenderer>();
        myShader = GetComponent<Shader>();
        myCollider = GetComponent<Collider>();
        //myGameObject = parent

        myCollider.isTrigger = true;
        //myCollider.includeLayers = 7;
        //myRigidBody = GetComponent<Rigidbody>();

        // expand rate applied here
        myVector3 = myVector3 * expandRate;

        // disable effect kinda
        transform.localScale = Vector3.zero;

        int stateCollider = LayerMask.NameToLayer("stateChangeCollision");
        //.layer = stateCollider;

        myRigidBody = GetComponent<Rigidbody>();

        if (myRigidBody != null)
        {

            myRigidBody = new Rigidbody();
            myRigidBody.excludeLayers = 0;
            //int stateCollider = LayerMask.NameToLayer("stateChangeCollision");
            //myRigidBody.layer = stateCollider;

            myRigidBody.includeLayers = 7;
            //myRigidBody.
            //myRigidBody.isKinematic = true;

        }

        // disable collider if there is one
        //if (!triggerChangeOnCollision)
        //{
        //    if (myCollider != null)
        //    {
        //        myCollider.enabled = false;
        //    }
        //}

        // add event delegate
        //myMove.timeChange += myInteraction;

        //myTypeCastTimer = (float)myTimer;
        myIncrement = (0.5f / ((float)myTimer));

        // do not expand on start
        tempTimer = myTimer + 1;

        // timescale increment setup
        TimeScaleLogIncrementPerFrame = (10 / (float)myTimer);

    }

    //private void {}

    private void OnDestroy()
    {   // remove event delegate
        //myMove.timeChange -= myInteraction;
    }

    public void myInteraction()
    {
        //slow mow!
        Time.timeScale = 0.5f;
        // enable sphere
        transform.localScale = Vector3.one;

        // reset timer
        tempTimer = 1;
        myThingy = 1f;


        if (myMove.currentTime)
        {
            //myInteractionNight();
            myMeshRenderer.material = myNightTexture;
            //myMeshRenderer.material.color = Color.red;
            //myMeshRenderer.material.set
        }
        else
        {
            //myInteractionDay();
            myMeshRenderer.material = myDayTexture;
            //myMeshRenderer.material.color = Color.blue;
        }

        //myColorIncrement = (myMeshRenderer.material.color.a / (float)myTimer);

    }


    // Update is called once per frame
    void Update()
    {
        if (tempTimer <= myTimer)
        {
            // debug statement
            
            //if (tempTimer % 100 == 0) { 
            //Debug.Log(tempTimer + ", " + myIncrement
            //    + ", " + Time.timeScale + ", " + myColorIncrement
            //    + ", " + myMeshRenderer.material.color.a);



            //myCollider.enabled = true;

            if (enableSlowMo)
            {
                // slow speed back up 
                Time.timeScale = Mathf.Log10(myThingy);
                // increment time scale
                myThingy += TimeScaleLogIncrementPerFrame;
            }
            // increment timer 
            tempTimer += 1;
            // EXPAND
            transform.localScale += myVector3;

            // sphere dissapears?
            //Color newColor = new Color(myOldColor.r,myOldColor.g,myOldColor.b,myOldColor.a - myTempIncrement);
            //myMeshRenderer.material.color = newColor;
            //myTempIncrement += myColorIncrement; 

            // SHADER DOES NOT HAVE PROPERTY COLOR
            /*
            myMeshRenderer.material.color = new Color(myMeshRenderer.material.color.r, myMeshRenderer.material.color.g, myMeshRenderer.material.color.b, 
                myMeshRenderer.material.color.a - myColorIncrement); 
            */


        }
        else
        { // timer is up 
            Time.timeScale = 1.0f;

            //myCollider.isTrigger = false;

            transform.localScale = Vector3.zero;


            //if (GetComponent<Collider>())
            //{
            //    // Active
            //    //myCollider.enabled = false;
            //}
            //int stateCollider = LayerMask.NameToLayer("stateChangeCollision");
            //gameObject.layer = stateCollider;


        }
    }
}
