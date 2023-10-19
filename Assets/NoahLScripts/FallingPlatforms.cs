using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private Rigidbody rigibod;

    public float fallTime;

    // Start is called before the first frame update
    void Start()
    {
        rigibod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());

        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallTime);
        rigibod.isKinematic = false;
    }
}
