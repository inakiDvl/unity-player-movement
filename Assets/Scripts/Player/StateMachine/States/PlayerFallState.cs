public class PlayerFallState : PlayerBaseState
{
    public override void UpdateState(PlayerStateManager player)
    {
        if (player.IsGrounded()) {
            player.ChangeState(player.playerIdleState);
        } else {
            player.SetGravity();
            player.ApplyMovementRelativeToCamera();
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.SetGravity();
    }
}
