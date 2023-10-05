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
    
    //              collision related variables
    [SerializeField] private LayerMask rayCastLayerMask;
    private float rayDistanceOffset = 0.5f;
    public bool drawRayCast = false;
    #endregion
    
    private void Update() {
        RotateCamera();
        HandleCameraCollision();
        MakeCameraFollowTarget();
    }

    private void LateUpdate() {
        
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
    }

    private void MakeCameraFollowTarget() 
    {   
        transform.position = cameraTarget.position - transform.forward * cameraDistanceToTarget;
    }
    #endregion

    #region         CAMERA COLLISION METHODS
    private void HandleCameraCollision()
    {
        Vector3 rayCastOrigin = cameraTarget.position;
        Vector3 rayCastDirection = (transform.position - rayCastOrigin).normalized;
        float rayDistance = 7f + rayDistanceOffset;

        RaycastHit hit;
        if (Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, rayDistance, rayCastLayerMask)) {
            float distanceToCollision = hit.distance;
            float distanceToAvoidCollision = cameraDistanceToTarget - distanceToCollision;
            cameraDistanceToTarget = Mathf.Lerp(cameraDistanceToTarget, cameraDistanceToTarget - distanceToAvoidCollision, 10f * Time.deltaTime);
        } else {
            cameraDistanceToTarget = Mathf.Lerp(cameraDistanceToTarget, 7f, 10f * Time.deltaTime);
        }

        if (drawRayCast) {
            Debug.DrawRay(rayCastOrigin, rayCastDirection * rayDistance, Color.green);
        }
    }
    #endregion      
}
