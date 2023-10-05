public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.SetJumpVelocity();
          
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.ApplyMovementRelativeToCamera();
        
        if (player.velocity < 0) {
            player.ChangeState(player.playerFallState);
        } else {
            player.ApplyRotationRelativeToCamera();    
            player.SetGravity();
        }   
    }
}
