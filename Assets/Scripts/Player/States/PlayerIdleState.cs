using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.speed = 0;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.IsTryingToMove()) {
            player.ChangeState(player.playerWalkState);
        } else {
            player.SetMoveDirectionRelativeToCamera();
            player.SetGravity();
            player.ApplyMovementRelativeToCamera();
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        //Debug.Log("exited idle state");
    }
}
