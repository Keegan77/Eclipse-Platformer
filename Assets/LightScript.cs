using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour 
{

    public bool activeDay;
    public bool activeNight;

    private Light myLight;
    private bool wait;
    private stateChangeEffect stateShift;
    private float increment;
    private float shadowInc;
    private int tempTimer;  

    private void Start()
    {
        stateShift = FindObjectOfType<stateChangeEffect>();
        stateShift.myShift += stateChange;
        myLight = GetComponent<Light>();

        myLight.lightmapBakeType = LightmapBakeType.Realtime;

        increment = (myLight.intensity / stateShift.myTimer);
        shadowInc = (myLight.shadowStrength / stateShift.myTimer);
        // if we are not active in the day and it is day 
        //                  and
        // if we are not active at night and it is night ... 
        // thats what this should be, but this is what works so idk
        if ((activeDay && stateShift.timeOfDay) || (activeNight && !stateShift.timeOfDay))
        {
            myLight.intensity = 0f;
            myLight.shadowStrength = 0f;
            return;
        }
        increment *= -1f;
        shadowInc *= -1f;
    }

    private void OnDestroy()
    {
        stateShift.myShift -= stateChange;
    }

    private void stateChange() 
    { 
        tempTimer = 0;
        wait = true;
    }

    private void Update()
    {
        if (wait)
        {
            if (tempTimer > stateShift.myTimer)
            {
                myLight.intensity = Mathf.Round(myLight.intensity);
                myLight.shadowStrength = Mathf.Round(myLight.shadowStrength);
                wait = false;
                increment *= -1f;
                shadowInc *= -1f;
            }
            myLight.intensity += increment;
            myLight.shadowStrength += shadowInc;
            tempTimer++;
        }

    }
}


