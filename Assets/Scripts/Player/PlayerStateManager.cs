using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayerStateManager : MonoBehaviour
{
    #region     STATE MACHINE VARIABLES
    public PlayerBaseState currentState;
    public PlayerIdleState playerIdleState = new PlayerIdleState();
    public PlayerWalkState playerWalkState = new PlayerWalkState();
    public PlayerRunState playerRunState = new PlayerRunState();
    public PlayerJumpState playerJumpState = new PlayerJumpState();
    public PlayerFallState playerFallState = new PlayerFallState();
    #endregion

    #region     PLAYER VARIABLES
    //          player variables
    public float speed;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private float jumpPower = 3f;

    //          gravity variables
    private float gravity = -9.81f;
    private float gravityMultiplayer = 3;
    public float velocity;

    //          movement variables
    public Camera playerCamera;
    private CameraLogic cameraLogic;
    public CharacterController characterController;
    private Vector2 moveInput;
    private Vector3 moveDirectionRelativeToCamera;
    private float rotationSpeed = 15f;
    public bool isRunPressed = false;

    #endregion

    private void Awake() {  
        cameraLogic = playerCamera.GetComponent<CameraLogic>();                 
    }                       

    private void Start() {
        velocity = gravity;// to prevent jumping before velocity reaches gravity

        currentState = playerIdleState;
        currentState.EnterState(this);
    }

    private void Update() {
        if (currentState != null) {
            currentState.UpdateState(this);
        }

        if (currentState != playerFallState && velocity < gravity - 2f) {
            ChangeState(playerFallState);
        }

        SetMoveDirectionRelativeToCamera();
    }

    #region     STATE MACHINE METHODS
    public void ChangeState(PlayerBaseState state) 
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
    #endregion

    #region     MOVEMENT RELATED METHODS
    public void ApplyMovementRelativeToCamera()
    {
        characterController.Move((moveDirectionRelativeToCamera.normalized * speed + Vector3.up * velocity) * Time.deltaTime);
    }

    public void SetJumpVelocity()
    {
        velocity = Mathf.Sqrt(jumpPower * -2f * gravity);
    }

    public void SetGravity()
    {
        if (IsGrounded()) {
            velocity = gravity;
        } else {
            velocity += gravity * gravityMultiplayer * Time.deltaTime;
        }
    }

    public void ApplyRotationRelativeToCamera()
    {
        Vector3 lookRotation;
        lookRotation.x = moveDirectionRelativeToCamera.x;
        lookRotation.y = 0.0f;
        lookRotation.z = moveDirectionRelativeToCamera.z;
        
        if (lookRotation != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(lookRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetMoveDirectionRelativeToCamera()
    {
        if (IsGrounded()) {
            // Get the camera's forward and right vectors
            Vector3 forward = playerCamera.transform.forward;
            Vector3 right = playerCamera.transform.right;

            // Remove the vertical component to move only in the horizontal plane
            forward.y = 0f;
            right.y = 0f;

            Vector3 forwardRelativeVerticalInput = moveInput.y * forward;
            Vector3 rightRelativeHorizontalInput = moveInput.x * right;

            moveDirectionRelativeToCamera = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
        }
        
    }

    public bool IsGrounded() 
    {
        return characterController.isGrounded;
    }
    #endregion

    #region     INPUT RELATED METHOS
    private void OnMove(InputValue inputValue)
    { 
        moveInput = inputValue.Get<Vector2>();
    }

    private void OnJump()
    {
        if (currentState != playerJumpState && IsGrounded()) {
            ChangeState(playerJumpState);
        }
    }
    
    private void OnRunPressed() 
    {
        if (currentState != playerRunState) {
            isRunPressed = true;
        }
    }

    private void OnRunReleased()
    {
        isRunPressed = false;
    }

    private void OnMoveCamera(InputValue inputValue)
    {
        cameraLogic.moveInput = inputValue.Get<Vector2>();
    }

    public bool IsTryingToMove()
    {
        if (moveDirectionRelativeToCamera != Vector3.zero) {
            return true;
        } else {
            return false;
        }
    }

    #endregion
}
