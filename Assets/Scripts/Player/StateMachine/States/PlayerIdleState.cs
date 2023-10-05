public class PlayerIdleState : PlayerBaseState
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
            //              movement related
            player.SetMoveDirectionRelativeToCamera();
            player.SetGravity();
            player.ApplyMovementRelativeToCamera();
            //              other
            player.InteractionArea();
        }
    }
}
