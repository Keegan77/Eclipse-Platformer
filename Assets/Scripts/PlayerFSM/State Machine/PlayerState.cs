using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine playerFsm;

    public PlayerState(Player player, PlayerStateMachine playerFsm) //class constructor
    {
        this.player = player;
        this.playerFsm = playerFsm;
    }

    public virtual void StateStart() { } // virtual means override is optional, but not forced

    public virtual void StateExit() { }

    public virtual void StateUpdate() { }

    public virtual void StateFixedUpdate() { }

    public virtual void StateCollisionEnter(Collision collision) { }

    public virtual void StateCollisionStay(Collision collision) { }

    public virtual void StateCollisionExit(Collision collision) { }
}
