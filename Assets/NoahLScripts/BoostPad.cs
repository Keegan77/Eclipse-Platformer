using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    public int BoostValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 private void OnCollisionStay(Collision other)
    {
        other.collider.GetComponent<Rigidbody>().AddForce(Vector3.up * BoostValue);
    }
}
