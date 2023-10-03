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
       RedRayCast();
       
       MoveCameraForward();
    }

    private void LateUpdate() {
        MakeCameraFollowTarget();
        GreenRayCast();
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
    public LayerMask rayCastLayerMask;
    public bool drawRayCast = false;
    [SerializeField] private bool grenRayCastCollisionDetected;
    [SerializeField] private bool redRayCastCollisionDetected;
    float rayDistanceOffset = 0.1f;

    private void RedRayCast()
    {
        Vector3 rayCastOrigin = cameraTarget.position;
        Vector3 rayCastDirection = (transform.position - rayCastOrigin).normalized;
        float rayDistance = Vector3.Distance(cameraTarget.position, transform.position) + rayDistanceOffset;

        RaycastHit hit;
        if (Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, rayDistance, rayCastLayerMask)) {
            redRayCastCollisionDetected = true;
        } else {
            redRayCastCollisionDetected = false;
        }

        if (drawRayCast) {
            Debug.DrawRay(rayCastOrigin, rayCastDirection * rayDistance, Color.red);
        }
    }

    private void GreenRayCast()
    {
        Vector3 rayCastOrigin = cameraTarget.position;
        Vector3 rayCastDirection = (transform.position - rayCastOrigin).normalized;
        float rayDistance = 7f + rayDistanceOffset;

        RaycastHit hit;
        if (Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, rayDistance, rayCastLayerMask)) {
            grenRayCastCollisionDetected = true;
        } else {
            grenRayCastCollisionDetected = false;
        }

        if (drawRayCast) {
            Debug.DrawRay(rayCastOrigin, rayCastDirection * rayDistance, Color.green);
        }
    }

    private void MoveCameraForward() 
    {
        if (grenRayCastCollisionDetected && redRayCastCollisionDetected) {
            cameraDistanceToTarget -= 0.05f;
        } else if (!grenRayCastCollisionDetected && !redRayCastCollisionDetected) {
            cameraDistanceToTarget = 7f;
        }
    } 
    #endregion
}
