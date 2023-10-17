using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerState
{
    public PlayerAirborneState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }
    float timer;
    float buffer = .3f;
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
        timer = 0;
    }

    public override void StateUpdate()
    {
        timer += Time.deltaTime;
        base.StateUpdate();

        player.speedTarget = player.maxSpeed *
                  (Mathf.Abs(player.input.Player.Movement.ReadValue<Vector2>().x)
                 + Mathf.Abs(player.input.Player.Movement.ReadValue<Vector2>().y)); //simple way of speed checking the joystick

        player.currentSpeed = Mathf.Lerp(player.currentSpeed,
                                         player.speedTarget,
                                         Time.deltaTime * player.accelSpeed);

        if (player.jumpInput == false)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Min(player.rb.velocity.y, player.jumpAmount / player.jumpCutMultiplier));
            //dynamic jump cutoff
        }

        if (player.CheckGround() && player.rb.velocity.y <= 0 && timer > buffer)
        {
            playerFsm.SwitchState(player.movementState);
            //Debug.Log("Switched");
        }
    }
}
