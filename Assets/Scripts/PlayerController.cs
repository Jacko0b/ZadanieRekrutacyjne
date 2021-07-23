using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private Camera playerCamera;
    private CharacterController controller;
    private Vector2 movementInput;
    private Vector2 rotateInput;
    private float xRotation;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float radiusOfGroundCheckSphere = 0.1f;
    private float velocity;
    private bool isGrounded;

    [SerializeField] private float raycastDistance = 5f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }
    public void FixedUpdate()
    {

        MovePlayer();
        RotatePlayer();
    }

    private void Shoot()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Ray ray = playerCamera.ScreenPointToRay(mouseScreenPosition);


        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.collider.CompareTag("object"))
            {
                if(raycastHit.distance < raycastDistance)
                {
                    Destroy(raycastHit.collider.transform.parent.gameObject);
                } 
            }

        }
    }
    private void IsGrounded()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, radiusOfGroundCheckSphere, groundMask);
    }
    private void RotatePlayer()
    {
        float mouseX = rotateInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = rotateInput.y * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
    private void MovePlayer()
    {
        IsGrounded();

        //movement
        Vector3 movementVector = transform.right * movementInput.x + transform.forward * movementInput.y;
        controller.Move(movementVector * movementSpeed * Time.deltaTime);

        //gravity 
        if(isGrounded && velocity < 0)
        {
            velocity = -0.01f;
        }
        velocity += gravity * Time.deltaTime * Time.deltaTime;
        controller.Move(new Vector3(0, velocity, 0));

    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnFire ( InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Shoot();
        }
    }
  


}
