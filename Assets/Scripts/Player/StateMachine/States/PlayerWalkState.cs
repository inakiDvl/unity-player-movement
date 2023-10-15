using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.speed = player.walkSpeed;
        Debug.Log("ENTER WALK");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.IsTryingToMove()) {
            player.ChangeState(player.playerIdleState);
        }

        if (player.isRunPressed) {
            player.ChangeState(player.playerRunState);
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
        Debug.Log("EXIT WALK");
    }
}
