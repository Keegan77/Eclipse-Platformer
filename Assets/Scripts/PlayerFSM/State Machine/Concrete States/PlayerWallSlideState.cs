using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }
    Input input;
    public RaycastHit hit;

    public override void StateStart()
    {
        base.StateStart();
        input = new Input();

        input.Enable();
        input.Player.Jump.performed += PerformWalljump;
        player.AnimationTriggerEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Wallslide]);
    }

    public override void StateExit()
    {
        base.StateExit();
        input.Disable();
        input.Player.Jump.performed -= PerformWalljump;
        player.AnimationFinishedEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Wallslide]);
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
    }



    private void PerformWalljump(InputAction.CallbackContext ctx)
    {
        player.walljumpState.hit = hit;
        playerFsm.SwitchState(player.walljumpState);

    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, 
                                    Quaternion.FromToRotation(Vector3.right, hit.normal), 
                                    Time.deltaTime * player.wallslideTurnSpeed);//player.transform.eulerAngles = Quaternion.Euler(Vector3.Lerp(Vector3.right, hit.normal, Time.time * player.wallslideTurnSpeed));

        player.rb.velocity = new Vector3(0, -player.wallslideSpeed * Time.deltaTime, 0);
        
        if (player.CheckGround())
        {
            playerFsm.SwitchState(player.idleState);
        }

    }
}
