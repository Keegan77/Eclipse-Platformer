using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
//using System.Collections;
using UnityEngine;
using UnityEngine.UI;


// this script attaches to the player object
//anchors?
public class stateChangeEffect : MonoBehaviour
{
    private myMove myMove;
    private MeshRenderer myMeshRenderer;
    public Material myDayTexture, myNightTexture;
    private Shader myShader;
    //private Rigidbody myRigidbody;

    //public Day2Night day2Night;

    public delegate void MyDelegate();
    public MyDelegate myShift;


    //private GameObject myGameObject;
    private Collider myCollider;


    // # of frames we expand for
    public int myTimer = 60;
    private int tempTimer;

    private float myTempIncrement;
    private float myIncrement;
    //private float myColorIncrement;

    private float myThingy = 1;
    private float myThingyIncrement = 0.1f;

    public float expandRate = 1.5f;
    Vector3 myVector3 = Vector3.one;



    // Start is called before the first frame update
    void Start()
    {
        myMove = GetComponent<myMove>();
        myMeshRenderer = GetComponent<MeshRenderer>();
        myShader = GetComponent<Shader>();
        myCollider = GetComponent<Collider>();
        //myRigidbody = GetComponent<Rigidbody>();

        // do not collider with any layers
        //myRigidbody = new Rigidbody();
        //myRigidbody.excludeLayers = 0;

        //        SphereCollider sc = gameObject.AddComponent<SphereCollider>();
        //myRigidbody = gameObject.AddComponent<Rigidbody>();
        //myRigidbody.excludeLayers = 0;
        //myRigidbody.includeLayers = 7;
        //myRigidbody.useGravity = false;
        //myRigidbody.isKinematic = ;
        

        // expand rate applied here
        myVector3 = myVector3 * expandRate;

        // disable effect kinda
        transform.localScale = Vector3.zero;
        myCollider.includeLayers = 7;

        // disable collider if there is one
        //if (myCollider != null) {
        //    myCollider.enabled = false;
        //}
        myCollider.isTrigger = true;
        // add event delegate
        //myMove.timeChange += myInteraction;

        //myTypeCastTimer = (float)myTimer;
        myIncrement = (0.5f / ((float)myTimer));

        tempTimer = myTimer + 1;
        myThingyIncrement = (10 / (float)myTimer);

    }

    private void OnDestroy()
    {   // remove event delegate
        //myMove.timeChange -= myInteraction;
        myShift = null;

    }

    //void OnCollisionEnter(Collision collision) {
    //    foreach (ContactPoint contact in collision.contacts)
    //    {
    //        //Debug.DrawRay(contact.point, contact.normal, Color.white);
    //    }
    //}

    private void myInteraction()
    {
        //slow mow!
        //Time.timeScale = 0.5f;
        // enable sphere
        transform.localScale = Vector3.one;

        // reset timer
        tempTimer = 1;
        myThingy = 1f + myThingyIncrement;


        if (myMove.currentTime)
        {
            //myInteractionNight();
            myMeshRenderer.material = myNightTexture;
            //myMeshRenderer.material.color = Color.red;
            //myMeshRenderer.material.set
            myMove.currentTime = !myMove.currentTime;
        }
        else
        {
            //myInteractionDay();
            myMeshRenderer.material = myDayTexture;
            //myMeshRenderer.material.color = Color.blue;
            myMove.currentTime = !myMove.currentTime;
        }

        //myColorIncrement = (myMeshRenderer.material.color.a / (float)myTimer);

    }
   
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("here");

        if (tempTimer <= myTimer)
        {
            // debug statement
            //if (tempTimer % 10 == 0) { 
            //Debug.Log(tempTimer + ", " + myIncrement
            //    + ", " + Time.timeScale + ", " /*+ myColorIncrement*/
            //    + ", " /*+ myMeshRenderer.material.color.a*/);
            //}
            Time.timeScale = Mathf.Log10(myThingy);
            // increment timer
            tempTimer += 1;
            myThingy += myThingyIncrement;
            // EXPAND
            transform.localScale += myVector3;
        }
        else
        { // timer is up 
            Time.timeScale = 1.0f;
            transform.localScale = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            myInteraction();
            myShift?.Invoke();
        }
    }
}
