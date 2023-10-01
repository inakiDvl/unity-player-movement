using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("entered falling state");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.IsGrounded()) {
            player.ChangeState(player.playerIdleState);
        } else {
            player.SetGravity();
            player.ApplyMovement();
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.SetGravity();
        //Debug.Log("exited falling state");
    }
}
