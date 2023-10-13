using UnityEngine;

public class CameraFreeState : CameraBaseState
{
    public override void UpdateState(CameraStateManager camera)
    {
        base.UpdateState(camera);

        if (Input.GetKeyDown(KeyCode.L)) {
            camera.ChangeState(camera.cameraLockedState);
        } else {
            camera.RotateCamera();
            camera.MakeCameraFollowTarget(0f);
        }
    }
}
