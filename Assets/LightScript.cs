using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour 
{

    public bool activeDay;
    public bool activeNight;
    public bool LightDecay;

    private Light myLight;
    private bool wait;
    private stateChangeEffect stateShift;
    private float increment;
    private int tempTimer;
    //private bool toDay;

    private void Start()
    {
        stateShift = FindObjectOfType<stateChangeEffect>();

        stateShift.myShift += stateChange;

        myLight = GetComponent<Light>();

        if (!activeDay || !stateShift.timeOfDay)
        {
            myLight.intensity = 0f;
        }
    }

    private void OnDestroy()
    {
        stateShift.myShift -= stateChange;
    }

    private void stateChange() { 

        //myLight.enabled = false;
        tempTimer = 0;

        if (stateShift.timeOfDay)
        {
            //toDay = false;

            if (activeDay)
            {
                wait = true;
                increment = 1f * myLight.intensity / stateShift.myTimer;
            }
            else {
                increment = -1f * (myLight.intensity / stateShift.myTimer);
                wait = true; 
            }
        }
        else if (!stateShift.timeOfDay) {
            //toDay = true;
            if (activeNight)
            {

                wait = true;
                increment = 1f * myLight.intensity / stateShift.myTimer;
            }
            else
            {
                increment = -1f * (myLight.intensity / stateShift.myTimer);
                wait = true;
            }
        }

    }

    private void Update()
    {
        if (wait)
        {
            if(tempTimer >= stateShift.myTimer)
            {
                //myLight.intensity = 
                if ((stateShift.timeOfDay && activeDay) || (!stateShift.timeOfDay && activeNight))
                {
                    myLight.intensity = 1;
                }
                else
                    myLight.intensity = 0;

                wait = false;
            }
            myLight.intensity += increment;
            tempTimer++;
        }

    }
}


