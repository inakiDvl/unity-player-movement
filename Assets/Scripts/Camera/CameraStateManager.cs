using UnityEngine;

public class CameraStateManager : MonoBehaviour
{
    public CameraBaseState currentState;
    public CameraFreeState cameraFreeState = new CameraFreeState();
    public CameraLockedState cameraLockedState = new CameraLockedState();

    #region         VARIABLES
    public Transform cameraTarget;
    private float cameraHorizontalSensivility = 250f;
    private float cameraVerticalSensivility = 200f;
    private float cameraDistanceToTarget = 7f;
    public Vector2 moveInput;
    public float rotationX;
    public float rotationY;

    private float smoothTime = 15f; 
    
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
    }

    #region         CAMERA MOVEMENT METHODS
    public void RotateCamera() {
        float inputY = moveInput.x * cameraHorizontalSensivility * Time.deltaTime;
        float inputX = moveInput.y * cameraVerticalSensivility * Time.deltaTime;
        
        rotationX += inputX;
        rotationY += inputY;

        rotationX = Mathf.Clamp(rotationX, -30f, 60f);

        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothTime * Time.deltaTime);
    }   

    public Transform target;

    public void RotateCameraArroundLockedTarget()
    {
        Vector3 targetDirection = target.position - transform.position;
        Quaternion lockedTargetRotation = Quaternion.LookRotation(targetDirection);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, lockedTargetRotation, smoothTime * Time.deltaTime);
    }

    public Transform playerTransform;
    public PlayerStateManager playerStateManager;
    public void RotatePlayerTowardsLockedTarget()
    {
        Vector3 directoToTarget = target.position - playerTransform.position;
        directoToTarget = new Vector3(directoToTarget.x, 0f, directoToTarget.z);
        Quaternion lockedTargetRotation = Quaternion.LookRotation(directoToTarget);
        playerTransform.rotation = lockedTargetRotation;
    }

    private float lerpedCameraYOffset = 0f;
    private float cameraYOffsetSmoothTime = 3f;
    public void MakeCameraFollowTarget(float cameraYOffset)
    {
        lerpedCameraYOffset = Mathf.Lerp(lerpedCameraYOffset, cameraYOffset, cameraYOffsetSmoothTime * Time.deltaTime);
        transform.position = cameraTarget.position - transform.forward * cameraDistanceToTarget + Vector3.up * lerpedCameraYOffset;
    }

    public void SetCameraZRotationToZero()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
    }
    #endregion

    #region         CAMERA COLLISION METHODS
    public void HandleCameraCollision()
    {
        Vector3 rayCastOrigin = cameraTarget.position;
        Vector3 rayCastDirection = (transform.position - rayCastOrigin).normalized;
        float rayDistance = 7f + rayDistanceOffset;
        
        RaycastHit hit;
        Ray raycast = new Ray(rayCastOrigin, rayCastDirection);
        if (Physics.Raycast(raycast, out hit, rayDistance, rayCastLayerMask)) {
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
