using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerGroundedBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.speed = player.walkSpeed;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.IsTryingToMove()) {
            player.ChangeState(player.playerIdleState);
        } else if (player.isRunPressed) {
            player.ChangeState(player.playerRunState);
        } else {
            player.ApplyRotation();
            player.SetMoveDirection();
            player.SetGravity();
            player.ApplyMovement();
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        base.ExitState(player);
    }
}
