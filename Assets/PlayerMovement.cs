using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    #region     VARIABLES
    //          player variables
    private float speed = 5f;
    private float jumpPower = 2f;

    //          gravity variables
    private float gravity = -9.81f;
    private float gravityMultiplayer = 2;
    private float velocity;

    //          movement variables
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Vector2 input;
    [SerializeField] private Vector3 moveDirection;

    private float rotationSpeed = 15f;
    private PlayerInput playerInput;
    #endregion

    private void Update() 
    {
        CalculateMoveDirection();
        ApplyRotation();
        Move();
        ApplyGravity();
    }

    #region     MOVEMENT RELATED METHODS
    private void Move()
    {
        characterController.Move((moveDirection * speed + Vector3.up * velocity) * Time.deltaTime);
    }

    private void ApplyRotation()
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

    private void ApplyGravity() 
    {
        if (IsGrounded()) {
            velocity = gravity;
        } else {
            velocity += gravity * gravityMultiplayer * Time.deltaTime;
        }
    }

    private void CalculateMoveDirection() {
        if (IsGrounded()) {
            moveDirection.x = input.x;
            moveDirection.z = input.y;
        }
    }

    private bool IsGrounded() 
    {
        return characterController.isGrounded;
    }
    
    #endregion

    #region     INPUT ACTIONS
    private void OnMove(InputValue inputValue)
    { 
        input = inputValue.Get<Vector2>();
    }

    private void OnJump()
    {
        if (IsGrounded()) {
            velocity = Mathf.Sqrt(jumpPower * -2f * gravity);
            Move();
        }
    }
    #endregion
}
