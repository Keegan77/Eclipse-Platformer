using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm) //class constructor
    {
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        player.rb.velocity = new Vector3(Mathf.Lerp(player.rb.velocity.x, 0, Time.deltaTime * player.deccelSpeed),
                                                    player.rb.velocity.y,
                                         Mathf.Lerp(player.rb.velocity.z, 0, Time.deltaTime * player.deccelSpeed));
        player.currentSpeed = 0;
        //this is for decceleration
    }

    public override void StateStart()
    {
        base.StateStart();

        player.AnimationTriggerEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Idle]);
    }
    public override void StateExit()
    {
        base.StateExit();

        player.AnimationFinishedEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Idle]);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (player.direction.magnitude > 0)
        {
            playerFsm.SwitchState(player.movementState);
        }

        if (!player.CheckGround())
        {
            playerFsm.SwitchState(player.airborneState);
        }
    }

}
