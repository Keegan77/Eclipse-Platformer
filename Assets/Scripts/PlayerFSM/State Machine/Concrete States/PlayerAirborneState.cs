using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerState
{
    public PlayerAirborneState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }
    bool jumpLetOff;

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
        if (player.direction.magnitude > 0)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(new Vector3(0, player.targetAngle, 0)), Time.deltaTime * player.turningSpeed);

            player.rb.velocity = new Vector3(player.movedirection.x * player.currentSpeed, player.rb.velocity.y, player.movedirection.z * player.currentSpeed); //affect movement

            //placeholder air movement code, copied from ground movement                                                                                                                                      //throwing ground movement in air for testing
        }

    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        player.currentSpeed = Mathf.Lerp(player.currentSpeed,
                                             player.maxSpeed,
                                             Time.deltaTime * player.accelSpeed);

        if (Input.GetKeyUp(KeyCode.Space) && !player.CheckGround())
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Min(player.rb.velocity.y, player.jumpAmount / player.jumpCutMultiplier));
            //dynamic jump cutoff
        }

        if (player.CheckGround() && player.rb.velocity.y < 0)
        {
            playerFsm.SwitchState(player.movementState);
        }
    }
}
