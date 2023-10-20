using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarChecker : MonoBehaviour
{

    public StarContainer starContainer;
    public Vector3 initPos;
    float sinMagnitude = 2;
    float sinDistance = .1f;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.time * sinMagnitude) * sinDistance) + initPos.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            starContainer.Stars += 1;
            Destroy(gameObject);
        }
    }
    //goes onto the star collectable
}
