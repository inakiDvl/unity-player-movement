using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("entered run state");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.isRunPressed || !player.IsTryingToMove()) {
            player.ChangeState(player.playerWalkState);
        } else {
            player.ApplyRotation();
            player.SetMoveDirection();          
            player.SetGravity();
            player.ApplyMovement();
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("exited run state");
    }
}
