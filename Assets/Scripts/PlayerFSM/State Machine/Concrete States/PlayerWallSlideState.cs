using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }
    public RaycastHit hit;
    public override void StateExit()
    {
        base.StateExit();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, 
                                    Quaternion.FromToRotation(Vector3.right, hit.normal), 
                                    Time.deltaTime * player.wallslideTurnSpeed);//player.transform.eulerAngles = Quaternion.Euler(Vector3.Lerp(Vector3.right, hit.normal, Time.time * player.wallslideTurnSpeed));

        player.rb.velocity = new Vector3(0, -player.wallslideSpeed * Time.deltaTime, 0);
        
    }
}
