using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    Player player;
    CheckpointManager checkpointMgr;

    private void Start()
    {
        checkpointMgr = FindObjectOfType<CheckpointManager>();
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = checkpointMgr.currentCheckpoint.position;
        }
    }

}
