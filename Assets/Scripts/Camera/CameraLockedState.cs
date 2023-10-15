using UnityEngine;

public class CameraLockedState : CameraBaseState
{
    public override void UpdateState(CameraStateManager camera)
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            camera.ChangeState(camera.cameraFreeState);
        } else {
            camera.RotateCameraArroundLockedTarget();
            camera.MakeCameraFollowTarget(1f);
            if (camera.playerStateManager.speed != camera.playerStateManager.runSpeed) {
                camera.RotatePlayerTowardsLockedTarget();
            }
        }
        base.UpdateState(camera);
    }

    public override void ExitState(CameraStateManager camera)
    {
        camera.rotationX = camera.transform.localEulerAngles.x;
        camera.rotationY = camera.transform.localEulerAngles.y;
    }
}
