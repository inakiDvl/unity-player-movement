using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLogic : MonoBehaviour
{
    #region         CAMERA RELATED VARIABLES
    public GameObject cameraFollow;
    private float cameraRotationSensivility = 200f;
    private float cameraFollowSpeed = 100f;
    
    public Vector2 moveInput;
    public float inputX;
    public float inputY;
    public float rotationX;
    public float rotationY;
    #endregion
    
    private void Update() {
       RotateCamera();
    }

    private void LateUpdate() {
        MakeCameraFollowTarget();
    }
    
    #region         CAMERA MOVEMENT METHODS
    private void RotateCamera() {
        inputY = moveInput.x;
        inputX = moveInput.y;
        
        rotationX += inputX * cameraRotationSensivility * Time.deltaTime;
        rotationY += inputY * cameraRotationSensivility * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -30f, 60f);
        
        Quaternion localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        transform.rotation = localRotation;
    }

    private void MakeCameraFollowTarget() 
    {   
        transform.position = Vector3.MoveTowards(transform.position, cameraFollow.transform.position, cameraFollowSpeed * Time.deltaTime);
    }
    #endregion
}
