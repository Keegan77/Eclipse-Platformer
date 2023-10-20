using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Video : MonoBehaviour
{

    private void Awake()

    {

        StartCoroutine(kill());

    }

    IEnumerator kill()

    {

        yield return new WaitForSeconds(7);

        Object.Destroy(this.gameObject);

    }
}

