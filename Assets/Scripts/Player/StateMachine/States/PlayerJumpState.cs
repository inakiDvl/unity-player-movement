public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.SetJumpVelocity();     
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.velocity < 0) {
            player.ChangeState(player.playerFallState);
        } 

        player.ApplyMovementRelativeToCamera();
        player.ApplyRotationRelativeToCamera();    
        player.SetGravity(); 
    }
}
