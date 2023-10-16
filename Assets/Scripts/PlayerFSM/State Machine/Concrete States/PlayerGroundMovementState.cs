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

        Debug.Log(player.direction.magnitude);

        player.currentSpeed = Mathf.Lerp(player.currentSpeed,
                                         player.maxSpeed,
                                         Time.deltaTime * player.accelSpeed);

        float targetAngle = Mathf.Atan2(player.direction.x, player.direction.y) * Mathf.Rad2Deg + player.cam.eulerAngles.y; // gives the target angle based on input
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(new Vector3(0, targetAngle, 0)), Time.deltaTime * player.turningSpeed);
        Vector3 movedirection = Quaternion.Euler(player.direction.x, targetAngle, player.direction.z) * Vector3.forward;
        player.rb.velocity = new Vector3(movedirection.x * player.currentSpeed, player.rb.velocity.y, movedirection.z * player.currentSpeed); //affect movement



    }

    public override void StateUpdate()
    {
        base.StateUpdate();

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
