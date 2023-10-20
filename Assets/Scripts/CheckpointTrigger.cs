using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    CheckpointManager checkpointMgr;
    private void Start()
    {
        checkpointMgr = FindObjectOfType<CheckpointManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointMgr.currentCheckpoint = transform;
        }
    }
}
