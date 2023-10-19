using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlidingState : PlayerState
{
    public PlayerSlidingState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }

    public override void StateCollisionEnter(Collision collision)
    {
        base.StateCollisionEnter(collision);
    }

    public override void StateExit()
    {
        base.StateExit();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();

        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(new Vector3(0, (player.targetAngle - player.cam.eulerAngles.y) + player.transform.eulerAngles.y, 0)), Time.deltaTime * player.airTurnControlSpeed);
        
        player.rb.velocity = new Vector3(Mathf.Lerp(player.rb.velocity.x, 0, Time.deltaTime * player.slideDeccelAmount),
                                                    player.rb.velocity.y,
                                         Mathf.Lerp(player.rb.velocity.z, 0, Time.deltaTime * player.slideDeccelAmount));
        player.currentSpeed = 0;
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (player.rb.velocity.x < 1 && player.rb.velocity.z < 1)
        {
            playerFsm.SwitchState(player.idleState);
        }
    }
}
