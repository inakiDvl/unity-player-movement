using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("ENTER IDLE");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.IsTryingToMove()) {
            if (player.isRunPressed) {
                player.ChangeState(player.playerRunState);
            } else {
                player.ChangeState(player.playerWalkState);
            }
        }
        //              movement related
        player.SetMoveDirectionRelativeToCamera();
        player.SetGravity();
        player.ApplyMovementRelativeToCamera();
        //              other
        player.InteractionArea();
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("EXIT IDLE");
    }
}
