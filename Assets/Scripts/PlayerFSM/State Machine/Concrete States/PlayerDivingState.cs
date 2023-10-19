using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDivingState : PlayerState
{
    public PlayerDivingState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }

    public override void StateStart()
    {
        base.StateStart();
        player.AnimationTriggerEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Dive]);
        
        player.rb.velocity = Vector3.zero;

        player.rb.AddForce(Vector3.up * player.verticalDiveHeight, ForceMode.Impulse);
        player.rb.AddForce(player.transform.forward * player.horizontalDiveAmount, ForceMode.Impulse);

    }

    public override void StateExit()
    {
        base.StateExit();
        player.AnimationFinishedEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Dive]);
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(new Vector3(0, (player.targetAngle - player.cam.eulerAngles.y) + player.transform.eulerAngles.y, 0)), Time.deltaTime * player.airTurnControlSpeed);
        player.rb.velocity = new Vector3(player.transform.forward.x * player.divingMaxSpeed, player.rb.velocity.y, player.transform.forward.z * player.divingMaxSpeed);
    }

    public override void StateCollisionEnter(Collision collision)
    {
        base.StateCollisionEnter(collision);

        playerFsm.SwitchState(player.slideState);

    }


    public override void StateUpdate()
    {
        base.StateUpdate();
        
    }
}
