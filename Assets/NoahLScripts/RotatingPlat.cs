using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlat : MonoBehaviour
{
    public Transform movingPlatform;
    public Transform position1;
    public Transform position2;
    public Quaternion newPosition;
    public string currentState;
    public float smooth;
    public float resetTime;

    // Use this for initialization
    void Start()
    {
        ChangeTarget();
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movingPlatform.rotation = Quaternion.Slerp(movingPlatform.rotation, newPosition, smooth * Time.deltaTime);
    }

    void ChangeTarget()
    {
        if (currentState == "Moving To Position 1")
        {
            currentState = "Moving To Position 2";
            newPosition = position2.rotation;
        }
        else if (currentState == "Moving To Position 2")
        {
            currentState = "Moving To Position 1";
            newPosition = position1.rotation;
        }
        else if (currentState == "")
        {
            currentState = "Moving To Position 2";
            newPosition = position2.rotation;
        }
        Invoke("ChangeTarget", resetTime);
    }
}
