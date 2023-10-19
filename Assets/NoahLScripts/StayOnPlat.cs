using UnityEngine;
using System.Collections;

public class HoldPlayer : MonoBehaviour
{

    void OnTriggerStay(Collider col)
    {
        col.transform.parent = gameObject.transform;
    }

    void OnTriggerExit(Collider col)
    {
        col.transform.parent = null;
    }
}