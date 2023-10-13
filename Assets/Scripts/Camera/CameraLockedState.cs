using UnityEngine;

public class CameraLockedState : CameraBaseState
{
    public override void UpdateState(CameraStateManager camera)
    {
        base.UpdateState(camera);
        
        if (Input.GetKeyDown(KeyCode.L)) {
            camera.ChangeState(camera.cameraFreeState);
        } else {
            camera.RotateCameraArroundLockedTarget();
            camera.MakeCameraFollowTarget(1f);
        }
    }
}
