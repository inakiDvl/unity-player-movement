public class PlayerWalkState : PlayerBaseState
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
            //              movement related
            player.ApplyRotationRelativeToCamera();
            player.SetMoveDirectionRelativeToCamera();
            player.SetGravity();
            player.ApplyMovementRelativeToCamera();
            //              other
            player.InteractionArea();
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        base.ExitState(player);
    }
}
