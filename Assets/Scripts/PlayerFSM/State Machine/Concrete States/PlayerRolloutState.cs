using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRolloutState : PlayerState
{
    public PlayerRolloutState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }
    float timer;
    float minimumStateDuration = .5f; //in seconds
    bool queueState = false;
    public override void StateStart()
    {
        base.StateStart();

        player.AnimationTriggerEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Rollout]);
        player.rb.AddForce(Vector3.up * player.rollHeight, ForceMode.Impulse);
        player.rb.AddForce(player.transform.forward * player.rolloutSpeed, ForceMode.Impulse);
    }
    public override void StateExit()
    {
        base.StateExit();
        player.SwitchCollisionsToNormal();
        player.AnimationFinishedEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Rollout]);
        queueState = false;
        timer = 0;
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        timer += Time.deltaTime;
        if (timer > minimumStateDuration)
        {
            playerFsm.SwitchState(player.movementState);
        }
    }
    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
    }




}
