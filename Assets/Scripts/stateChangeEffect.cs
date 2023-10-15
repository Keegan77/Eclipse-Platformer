using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
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


    //private GameObject myGameObject;
    private Collider myCollider;


    // # of frames we expand for
    public int myTimer = 60;
    private int tempTimer;

    private float myTempIncrement;
    private float myIncrement;
    private float myColorIncrement;

    private float myThingy = 0;
    private float myThingyIncrement = 0;

    public float expandRate = 1.5f;
    Vector3 myVector3 = Vector3.one;



    // Start is called before the first frame update
    void Start()
    {
        myMove = GetComponent<myMove>();
        myMeshRenderer = GetComponent<MeshRenderer>();
        myShader = GetComponent<Shader>();

        // expand rate applied here
        myVector3 = myVector3 * expandRate;

        // disable effect kinda
        transform.localScale = Vector3.zero;

        // disable collider if there is one
        if (myCollider != null) {
            myCollider.enabled = false;
        }
        // add event delegate
        myMove.timeChange += myInteraction;

        //myTypeCastTimer = (float)myTimer;
        myIncrement = (0.5f / ((float)myTimer));

        tempTimer = myTimer + 1;
        myThingyIncrement = (10 / (float)myTimer);

    }

    private void OnDestroy()
    {   // remove event delegate
        myMove.timeChange -= myInteraction;
    }

    private void myInteraction()
    {
        //slow mow!
        Time.timeScale = 0.5f;
        // enable sphere
        transform.localScale = Vector3.one;

        // reset timer
        tempTimer = 1;
        myThingy = 0.1f;


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

        myColorIncrement = (myMeshRenderer.material.color.a / (float)myTimer);

    }
    /*
     * 
     *  FUCKING STACK OVERFLOW SHIT
     * 
    public void ChangeDefaultMatAlpha(float a)
    {
        _MyMaterial.color = new Color(_MyMaterial.color.r, _MyMaterial.color.g _MyMaterial.color.b,
            a);
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (tempTimer <= myTimer)
        {
            // debug statement
            if (tempTimer % 10 == 0) { 
            Debug.Log(tempTimer + ", " + myIncrement
                + ", " + Time.timeScale + ", " /*+ myColorIncrement*/
                + ", " /*+ myMeshRenderer.material.color.a*/);

            }
            //time resumes to normal over time
            //Time.timeScale = 0.5f + (myIncrement * tempTimer);
            Time.timeScale = Mathf.Log10(myThingy);
            // increment timer
            tempTimer += 1;
            myThingy += myThingyIncrement;
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

            transform.localScale = Vector3.zero;
        }
    }
}
