using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalljumpState : PlayerState
{
    public PlayerWalljumpState(Player player, PlayerStateMachine playerFsm) : base(player, playerFsm)
    {
    }
    public bool southWall;
    public bool northWall;
    public bool westWall;
    public bool eastWall;

    public RaycastHit hit;

    public override void StateStart()
    {
        base.StateStart();


        player.rb.AddForce(player.transform.right * player.walljumpDistance, ForceMode.Impulse);
        player.rb.AddForce(Vector3.up * player.walljumpHeight, ForceMode.Impulse);
        player.AnimationTriggerEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Jump]);
    }

    public override void StateExit()
    {
        base.StateExit();

        player.AnimationFinishedEvent(PlayerAnims.AnimationTriggers[PlayerAnims.AnimationNames.Jump]);
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();

        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        if (hit.normal.z != -1)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation,
                                    Quaternion.FromToRotation(Vector3.forward, hit.normal),
                                    Time.deltaTime * player.wallslideTurnSpeed);
        }
        else
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation,
                                    new Quaternion(0, -180, 0, 0),
                                    Time.deltaTime * player.wallslideTurnSpeed);
        }


        if (player.direction.magnitude > 0)
        {
            //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(new Vector3(0, player.targetAngle, 0)), Time.deltaTime * player.airTurnControlSpeed);
            player.rb.velocity += new Vector3(player.movedirection.x * player.currentSpeed, 0, player.movedirection.z * player.currentSpeed);
            if (Mathf.Abs(player.rb.velocity.x) > player.maxSpeed)
            {
                player.rb.velocity = new Vector3(player.maxSpeed * Mathf.Sign(player.rb.velocity.x), player.rb.velocity.y, player.rb.velocity.z);
            }
            if (Mathf.Abs(player.rb.velocity.z) > player.maxSpeed)
            {
                player.rb.velocity = new Vector3(player.rb.velocity.x, player.rb.velocity.y, player.maxSpeed * Mathf.Sign(player.rb.velocity.z));
            }
            //player.rb.velocity = new Vector3(player.transform.forward.x * player.currentSpeed, player.rb.velocity.y, player.transform.forward.z * player.currentSpeed); //affect movement
            //player.rb.velocity = new Vector3(player.movedirection.x * player.currentSpeed, player.rb.velocity.y, player.movedirection.z * player.currentSpeed);                                                                                                                                     //throwing ground movement in air for testing
        }
        else //deccel
        {
            player.rb.velocity = new Vector3(Mathf.Lerp(player.rb.velocity.x, 0, Time.deltaTime * player.airDeccelSpeed),
                                            player.rb.velocity.y,
                                 Mathf.Lerp(player.rb.velocity.z, 0, Time.deltaTime * player.airDeccelSpeed));
            player.currentSpeed = 0;
        }



    }



    public override void StateUpdate()
    {
        base.StateUpdate();        
        
        player.speedTarget = player.maxSpeed *
                (Mathf.Abs(player.input.Player.Movement.ReadValue<Vector2>().x)
               + Mathf.Abs(player.input.Player.Movement.ReadValue<Vector2>().y)); //simple way of speed checking the joystick

        player.currentSpeed = Mathf.Lerp(player.currentSpeed,
                                         player.speedTarget,
                                         Time.deltaTime * player.accelSpeed / 5);


        if (player.CheckGround())
        {
            playerFsm.SwitchState(player.movementState);
            //Debug.Log("Switched");
        }

    }

    public override void StateCollisionEnter(Collision collision)
    {
        base.StateCollisionEnter(collision);
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 1.5f) && !player.CheckGround() && player.rb.velocity.y < 3)
        {
            player.wallslideState.hit = hit;
            playerFsm.SwitchState(player.wallslideState);
        }
    }


}
