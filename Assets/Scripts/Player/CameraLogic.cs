using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLogic : MonoBehaviour
{
    #region         CAMERA RELATED VARIABLES
    public Transform cameraTarget;
    private float cameraHorizontalSensivility = 250f;
    private float cameraVerticalSensivility = 200f;
    private float cameraDistanceToTarget = 7f;
    public Vector2 moveInput;
    private float inputX;
    private float inputY;
    private float rotationX;
    private float rotationY;
    private Vector3 currentRotation;
    private Vector3 currentVelocity = Vector3.zero;
    private float smoothTime = 0.05f;
    #endregion
    
    private void Update() {
       RotateCamera();
    }

    private void LateUpdate() {
        MakeCameraFollowTarget();
    }
    
    #region         CAMERA MOVEMENT METHODS
    private void RotateCamera() {
        inputY = moveInput.x * cameraHorizontalSensivility * Time.deltaTime;
        inputX = moveInput.y * cameraVerticalSensivility * Time.deltaTime;
        
        rotationX += inputX;
        rotationY += inputY;

        rotationX = Mathf.Clamp(rotationX, -30f, 60f);
        
        Vector3 targetRotation = new Vector3(rotationX, rotationY);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref currentVelocity, smoothTime);

        transform.localEulerAngles = currentRotation;
        //transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
    }

    private void MakeCameraFollowTarget() 
    {   
        transform.position = cameraTarget.position - transform.forward * cameraDistanceToTarget;
    }
    #endregion
}
