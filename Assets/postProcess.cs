using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postProcess : MonoBehaviour
{
    // Start is called before the first frame update
    private stateChangeEffect stateShift;

    public bool activeDay;

    //public Material nightBox;
    //public Material dayBox;

    // Start is called before the first frame update
    void Start()
    {
        stateShift = FindObjectOfType<stateChangeEffect>();
        stateShift.myShift += stateChange;
    }

    private void OnDestroy()
    {
        stateShift.myShift -= stateChange;
    }

    private void stateChange()
    {
        //if (stateShift.timeOfDay)
            //RenderSettings.skybox = dayBox;
        //else
            //RenderSettings.skybox = nightBox;
        if (activeDay)
        {
            enabled = true;
        }
        else
        {
            enabled = false;
        }
    }



}

