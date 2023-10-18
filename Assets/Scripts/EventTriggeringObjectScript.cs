using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.PhysicsModule;

public class EventTriggeringObjectScript : MonoBehaviour
{

    stateChangeEffect stateChangeEffect;

    //UnityEvent ColliderInfo;
    Collider myCollider;
    MeshRenderer myMeshRenderer;
    Rigidbody myRigidBody;
    UnityEvent changeState = new UnityEvent();


    GameObject myChild;
    //GameObject myParent;

    // PRIVATE VARIABLES
    private bool isDay;
    private Color myColor = new Color(0, 0, 0, 0);
    //--------------------

    // PUBLIC VARIABLES
    // the layer for collision is 0?
    public bool dayCollisionWithPlayer = true;
    public bool nightCollisionWithPlayer = false;
    public Material dayMat, nightMat;
    // -------------------

    public void MyDelegate()
    {// my interaction from Day2Night
        if (isDay)
        {
            // if collision is on during day include the default layer
            if (dayCollisionWithPlayer)
                myCollider.includeLayers = 0;
            else
                myCollider.excludeLayers = 0;
            //isDay = false;
            myMeshRenderer.material = dayMat;

        }
        else
        {
            // if collision is on at night include the default layer
            if (nightCollisionWithPlayer)
                myCollider.includeLayers = 0;
            else
                myCollider.excludeLayers = 0;
            //isDay = true;
            myMeshRenderer.material = nightMat;

        }


    }

    private void OnTriggerEnter(Collider col)
    {
        //then inform to All scripts or function that collide with points
        //ColliderInfo?.Invoke();
        Debug.Log("I was collided with");
        myCollider.excludeLayers = 7;
        MyDelegate();
    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log("I have exited collision");
        myCollider.includeLayers = 7;

    }

    // Start is called before the first frame update
    void Start()
    {

        //if (ColliderInfo != null)
        //ColliderInfo.AddListener(MyDelegate);

        //myChild.transform.parent = //myParent.transform;

        //int stateCollider = LayerMask.NameToLayer("stateChangeCollision");
        //myChild.layer = stateCollider;
        //if
        //myChild.GetComponent<MeshRenderer>()
        //Collider childCollider = new Collider();
        //childCollider.transform = myCollider.transform;
        //childCollider.excludeLayers = 0;
        //childCollider.includeLayers = 7;
        //myChild.layer
        
        //myChild.gameObject.layer = stateCollider;

        myCollider = GetComponent<Collider>();
        //myCollider.includeLayers = 7;
        myMeshRenderer = GetComponent<MeshRenderer>();

        //myCollider.

        //changeState.AddListener(MyDelegate);

        myRigidBody = GetComponent<Rigidbody>();

        if (myRigidBody != null)
        {

            myRigidBody = new Rigidbody();
            myRigidBody.excludeLayers = 0;
            //int stateCollider = LayerMask.NameToLayer("stateChangeCollision");
            //myRigidBody.layer = stateCollider;

            myRigidBody.includeLayers = 7;
            //myRigidBody.isKinematic = true;

        }

        if (myCollider != null)
        {
            //myCollider.includeLayers = 1; // include second layer
            //myCollider.isTrigger = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
