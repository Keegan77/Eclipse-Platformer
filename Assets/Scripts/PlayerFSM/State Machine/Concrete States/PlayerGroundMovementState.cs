using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundMovementState : PlayerState
{
    public PlayerGroundMovementState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm) { }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();


        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(new Vector3(0, player.targetAngle, 0)), Time.deltaTime * player.turningSpeed);

        

        player.rb.velocity = new Vector3(player.movedirection.x * player.currentSpeed, player.rb.velocity.y, player.movedirection.z * player.currentSpeed); //affect movement



    }
    
    private float LerpSpeed()
    {
        return Mathf.Lerp(player.currentSpeed,
                   player.speedTarget,
                   Time.deltaTime * player.accelSpeed);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        player.speedTarget = player.maxSpeed * 
            (Mathf.Abs(player.input.Player.Movement.ReadValue<Vector2>().x)
           + Mathf.Abs(player.input.Player.Movement.ReadValue<Vector2>().y)); //simple way of speed checking the joystick

        player.currentSpeed = LerpSpeed();
        player.animator.speed = LerpSpeed() / player.maxSpeed;

        if (!player.CheckGround())
        {
            playerFsm.SwitchState(player.airborneState); //switch to airborne with no jump
        }

        if (player.direction.magnitude == 0)
        {
            playerFsm.SwitchState(player.idleState);
        }
    }

    public override void StateStart()
    {
        base.StateStart();

        player.AnimationTriggerEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Run]);

    }

    public override void StateExit()
    {
        player.AnimationFinishedEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Run]);
        player.animator.speed = 1;
        base.StateExit();
    }
}
