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

        if (stateShift.timeOfDay && !activeDay)//(activeDay && stateShift.timeOfDay) || (activeNight && !stateShift.timeOfDay))
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
        myLight.intensity = 0f;
        myLight.shadowStrength = 0f;

        if (stateShift.timeOfDay)
        {
            if (activeDay)
            {
                myLight.intensity = 1f;
                myLight.shadowStrength = 1f;
            }
        }
        else
        {
            if (!activeDay)
            {
                myLight.intensity = 1f;
                myLight.shadowStrength = 1f;
            }
        }
        return;

    }

    private void Update()
    {
        if (wait)
        {
            Debug.Log(myLight.shadowStrength);
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


