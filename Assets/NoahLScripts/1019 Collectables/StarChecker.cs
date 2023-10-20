using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarChecker : MonoBehaviour
{

    public StarContainer starContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            starContainer.Stars += 1;
        }
    }
    //goes onto the star collectable
}
