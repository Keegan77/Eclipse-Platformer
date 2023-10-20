using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChange : MonoBehaviour
{

    private stateChangeEffect stateShift;

    // Start is called before the first frame update
    void Start()
    {
        stateShift.myShift += stateChange;
    }

    private void OnDestroy()
    {
        stateShift.myShift -= stateChange;
    }

    private void stateChange()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
