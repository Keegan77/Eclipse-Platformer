using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundMovementState : PlayerState
{
    public PlayerGroundMovementState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm) { }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType anim)
    {
        base.AnimationTriggerEvent(anim);
    }
    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();


        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(new Vector3(0, player.targetAngle, 0)), Time.deltaTime * player.turningSpeed);

        

        player.rb.velocity = new Vector3(player.movedirection.x * player.currentSpeed, player.rb.velocity.y, player.movedirection.z * player.currentSpeed); //affect movement



    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        

        player.currentSpeed = Mathf.Lerp(player.currentSpeed,
                                         player.maxSpeed,
                                         Time.deltaTime * player.accelSpeed);

        if (player.CheckGround() && player.jumpInput)
        {
            player.Jump();//switches to jump state
        }
        else if (!player.CheckGround() && !player.jumpInput )
        {
            playerFsm.SwitchState(player.airborneState); //switch to airborne with no jump
        }

        if (player.direction.magnitude == 0)
        {
            playerFsm.SwitchState(player.idleState);
        }
    }
}
