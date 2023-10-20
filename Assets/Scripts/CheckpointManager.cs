using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] Transform[] checkpointLocations;
    [HideInInspector] public Transform currentCheckpoint;

    private void Start()
    {
        currentCheckpoint = checkpointLocations[0];
    }
    private void Update()
    {
        Debug.Log(currentCheckpoint.position);
    }
}
