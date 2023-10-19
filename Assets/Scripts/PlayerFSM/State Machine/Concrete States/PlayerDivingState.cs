using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDivingState : PlayerState
{
    public PlayerDivingState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }


    public override void StateExit()
    {
        base.StateExit();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}
