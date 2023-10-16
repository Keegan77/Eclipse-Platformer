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
            player.currentSpeed = Mathf.Lerp(player.currentSpeed,
                                             player.maxSpeed,
                                             Time.deltaTime * player.accelSpeed);

            float targetAngle = Mathf.Atan2(player.direction.x, player.direction.y) * Mathf.Rad2Deg + player.cam.eulerAngles.y; // gives the target angle based on input
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(new Vector3(0, targetAngle, 0)), Time.deltaTime * player.turningSpeed);
            Vector3 movedirection = Quaternion.Euler(player.direction.x, targetAngle, player.direction.z) * Vector3.forward;
            player.rb.velocity = new Vector3(movedirection.x * player.currentSpeed, player.rb.velocity.y, movedirection.z * player.currentSpeed); //affect movement
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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Min(player.rb.velocity.y, player.jumpAmount / player.jumpCutMultiplier));
            //dynamic jump cutoff
        }

        if (player.CheckGround())
        {
            playerFsm.SwitchState(player.movementState);
        }
    }
}
