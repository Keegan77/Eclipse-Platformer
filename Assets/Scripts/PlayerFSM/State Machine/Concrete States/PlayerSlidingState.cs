using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlidingState : PlayerState
{
    public PlayerSlidingState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }
    Input input;
    float timer;
    float minimumStateDuration = .5f; //in seconds

    public override void StateCollisionEnter(Collision collision)
    {
        base.StateCollisionEnter(collision);
    }



    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();

        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(new Vector3(0, (player.targetAngle - player.cam.eulerAngles.y) + player.transform.eulerAngles.y, 0)), Time.deltaTime * player.airTurnControlSpeed);
        
        player.rb.velocity = new Vector3(Mathf.Lerp(player.rb.velocity.x, 0, Time.deltaTime * player.slideDeccelAmount),
                                                    player.rb.velocity.y,
                                         Mathf.Lerp(player.rb.velocity.z, 0, Time.deltaTime * player.slideDeccelAmount));
    }

    public override void StateStart()
    {
        base.StateStart();
        input = new Input();
        player.rb.AddForce(player.transform.forward * (player.slideInitialSpeed), ForceMode.Force);

        input.Enable();
        input.Player.Dive.performed += PerformRollout;
    }

    public override void StateExit()
    {
        base.StateExit();
        timer = 0;
        input.Disable();
    }

    private void PerformRollout(InputAction.CallbackContext ctx)
    {
        playerFsm.SwitchState(player.rolloutState);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        timer += Time.deltaTime;

        if (player.rb.velocity.x < .5f && player.rb.velocity.z < .5f && timer > minimumStateDuration)
        {
            playerFsm.SwitchState(player.idleState);
            player.SwitchCollisionsToNormal();
        }
    }
}
