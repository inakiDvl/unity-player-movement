using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("ENTER RUN");
        player.speed = player.runSpeed;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.IsTryingToMove()) {
            player.ChangeState(player.playerIdleState);
        }

        if (!player.isRunPressed) {
            player.ChangeState(player.playerWalkState);
        }

        //              movement related
        player.ApplyRotationRelativeToCamera();
        player.SetMoveDirectionRelativeToCamera();          
        player.SetGravity();
        player.ApplyMovementRelativeToCamera();
        //              other
        player.InteractionArea();
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("EXIT RUN");
    }
}
