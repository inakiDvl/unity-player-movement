using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("entered walk state");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.IsTryingToMove()) {
            player.ChangeState(player.playerIdleState);
        } else {
            player.ApplyRotation();
            player.SetMoveDirection();
            player.SetGravity();
            player.ApplyMovement();
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        //Debug.Log("exited walk state");
    }
}
