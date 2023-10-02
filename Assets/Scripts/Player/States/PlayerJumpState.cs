using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("entered jump state");
        player.SetJumpVelocity();
          
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.ApplyMovement();
        
        if (player.velocity < 0) {
            player.ChangeState(player.playerFallState);
        } else {
            player.ApplyRotation();    
            player.SetGravity();
        }   
    }

    public override void ExitState(PlayerStateManager player)
    {
        //Debug.Log("exited jump state");
    }
}
