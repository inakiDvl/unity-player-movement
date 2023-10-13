using UnityEngine;

public class CameraBaseState
{
    public virtual void EnterState(CameraStateManager camera) {}

    public virtual void UpdateState(CameraStateManager camera) 
    {
        camera.HandleCameraCollision();
    }

    public virtual void ExitState(CameraStateManager camera) {}

}
