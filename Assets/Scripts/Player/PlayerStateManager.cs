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
    /* [SerializeField] private */ public float velocity;

    //          movement variables
    public Camera playerCamera;
    [SerializeField] private CharacterController characterController;
    public Vector2 moveInput;
    public Vector3 moveDirection;
    public Vector3 moveDirectionRelativeToCamera;
    private float rotationSpeed = 15f;
    public bool isRunPressed = false;
    #endregion

    private void Awake() {  // when te player spawns vefore it reaches the ground velocity
        velocity = gravity; // statck starting at 0, thats while u are able to jump              
    }                       // between 0 and -9.81. To prevent this we do velocity = gravity

    private void Start() {
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

    public void ApplyMovement()
    {
        characterController.Move((moveDirection.normalized * speed + Vector3.up * velocity) * Time.deltaTime);
    }

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

    public void ApplyRotation()
    {
        Vector3 lookRotation;
        lookRotation.x = moveDirection.x;
        lookRotation.y = 0.0f;
        lookRotation.z = moveDirection.z;
        
        if (lookRotation != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(lookRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetMoveDirection() {
        if (IsGrounded()) {
            moveDirection.x = moveInput.x;
            moveDirection.z = moveInput.y;
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

            // Normalize the vectors to ensure consistent speed in all directions
            /* forward.Normalize();
            right.Normalize(); */

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
        if (currentState != playerJumpState && currentState is PlayerGroundedBaseState) {
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

    public bool IsTryingToMove()
    {
        if (moveDirection != Vector3.zero) {
            return true;
        } else {
            return false;
        }
    }

    #endregion
}
