using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("entered idle state");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.IsTryingToMove()) {
            player.ChangeState(player.playerWalkState);
        } else {
            player.SetMoveDirection();
            player.SetGravity();
            player.ApplyMovement();
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        //Debug.Log("exited idle state");
    }
}
