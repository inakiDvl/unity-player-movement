using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.speed = player.runSpeed;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.isRunPressed) {
            player.ChangeState(player.playerWalkState);
        } else {
            player.ApplyRotationRelativeToCamera();
            player.SetMoveDirectionRelativeToCamera();          
            player.SetGravity();
            player.ApplyMovementRelativeToCamera(); 
        }
    }

    public override void ExitState(PlayerStateManager player)
    {

    }
}
