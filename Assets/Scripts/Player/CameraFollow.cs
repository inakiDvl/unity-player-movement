using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    private float cameraSensivility = 200f;
    private float cameraMoveSpeed = 100f;
    public GameObject cameraFollow;
    public Vector2 moveInput;
    public float inputX;
    public float inputY;
    public float rotationX;
    public float rotationY;

    private void Update() {
        SetMoveDirection();

        rotationX += inputX * cameraSensivility * Time.deltaTime;
        rotationY += inputY * cameraSensivility * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -30f, 60f);

        Quaternion localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        transform.rotation = localRotation;
    }

    private void LateUpdate() {
        Transform target = cameraFollow.transform;

        transform.position = Vector3.MoveTowards(transform.position, target.position, cameraMoveSpeed * Time.deltaTime);
    }

    #region     INPUT RELATED METHODS
    private void OnMoveCamera(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    public void SetMoveDirection() {
        inputY = moveInput.x;
        inputX = moveInput.y;
    }
    #endregion
}
