using UnityEngine;

public class CameraStateManager : MonoBehaviour
{
    public CameraBaseState currentState;
    public CameraFreeState cameraFreeState = new CameraFreeState();
    public CameraLockedState cameraLockedState = new CameraLockedState();

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
    public Vector3 currentRotation;
    private Vector3 currentVelocity = Vector3.zero;
    private float smoothTime = 10f;   
    
    //              collision related variables
    [SerializeField] private LayerMask rayCastLayerMask;
    private float rayDistanceOffset = 0.5f;
    public bool drawRayCast = false;
    #endregion

    private void Start() 
    {
        currentState = cameraFreeState;
        currentState.EnterState(this);
    }

    private void Update() 
    {
        if (currentState != null) {
            currentState.UpdateState(this);
        }
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, smoothTime * Time.deltaTime);
    }

    #region         CAMERA MOVEMENT METHODS
    Vector3 targetRotation;
    public void RotateCamera() {
        inputY = moveInput.x * cameraHorizontalSensivility * Time.deltaTime;
        inputX = moveInput.y * cameraVerticalSensivility * Time.deltaTime;
        
        rotationX += inputX;
        rotationY += inputY;

        rotationX = Mathf.Clamp(rotationX, -30f, 60f);

        /* Vector3 targetRotation = new Vector3(rotationX, rotationY);

        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref currentVelocity, smoothTime * Time.deltaTime);
        transform.localEulerAngles = currentRotation; */

        targetRotation = new Vector3(rotationX, rotationY);
        
        
        transform.localEulerAngles = currentRotation;
    }

    public Transform target;
    public void RotateCameraArroundLockedTarget()
    {
        Vector3 targetDirection = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothTime * Time.deltaTime);
    }

    float lerpedY = 0f;
    public void MakeCameraFollowTarget(float y) 
    {
        lerpedY = Mathf.Lerp(lerpedY, y, 5f * Time.deltaTime);
        transform.position = cameraTarget.position - transform.forward * cameraDistanceToTarget + Vector3.up * lerpedY;
    }
    #endregion

    #region         CAMERA COLLISION METHODS
    public void HandleCameraCollision()
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

    #region         STATE METHODS
    public void ChangeState(CameraBaseState state) 
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
    #endregion
}
