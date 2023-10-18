using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRolloutState : PlayerState
{
    public PlayerRolloutState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType anim)
    {
        base.AnimationTriggerEvent(anim);
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
