using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentPlayerState;

    public void Init(PlayerState startingState)
    {
        currentPlayerState = startingState;
        currentPlayerState.StateStart();
    }

    public void SwitchState(PlayerState nextState)
    {
        currentPlayerState.StateExit();
        currentPlayerState = nextState;
        currentPlayerState.StateStart();
    }
}
