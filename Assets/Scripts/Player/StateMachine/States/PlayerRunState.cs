public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.speed = player.runSpeed;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.isRunPressed || !player.IsTryingToMove()) {
            player.ChangeState(player.playerWalkState);
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
}
