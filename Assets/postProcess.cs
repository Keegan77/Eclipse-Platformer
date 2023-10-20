using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class postProcess : MonoBehaviour
{
    // Start is called before the first frame update
    private stateChangeEffect stateShift;
    private GameObject myObject;

    public bool activeDay;

    //public Material nightBox;
    //public Material dayBox;

    // Start is called before the first frame update
    void Start()
    {
        stateShift = FindObjectOfType<stateChangeEffect>();
        stateShift.myShift += stateChange;

        if (stateShift.timeOfDay && !activeDay)
        {
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(true);

        //myObject = gameObject.find
    }

    private void OnDestroy()
    {
        stateShift.myShift -= stateChange;
    }

    private void stateChange()
    {
        //enabled = stateShift.timeOfDay ? true : false; 
        //gameObject.enabled = false;
        //if (stateShift.timeOfDay)
        //{
        //if (stateShift.timeOfDay && !activeDay)
        //{
        //    gameObject.SetActive(false);
        //}
        //else if (!stateShift.timeOfDay && activeDay)
        //{
        //    gameObject.SetActive(false);
        //}
        gameObject.SetActive(false );

        if (stateShift.timeOfDay)
        {
            if (activeDay)
            {
                gameObject.SetActive(true );
            }
        }
        else
        {
            if(!activeDay)
            {
                gameObject.SetActive(true);
            }
        }

        //}
        //else
        //{
        //    if (!activeDay)
        //    {
        //        gameObject.SetActive(true);
        //    }
        //    else
        //        gameObject.SetActive(false);
        //}

    }



}

