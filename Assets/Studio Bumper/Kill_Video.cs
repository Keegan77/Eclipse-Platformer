using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Video : MonoBehaviour
{

    public float vidTime;

    private void Awake()

    {

        StartCoroutine(kill());

    }

    IEnumerator kill()

    {

        yield return new WaitForSeconds(vidTime);

        Object.Destroy(this.gameObject);

    }
}

