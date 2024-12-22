using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerMov : MonoBehaviour
{
   private Rigidbody capsuleRigidBody;
    private CapsuleController capsuleControllerActions;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] LayerMask ground;
    private bool isForwardPressed;
    private bool isBackwardPressed;
    private bool isRightPressed;
    private bool isLeftPressed;
    private bool isJumping;

    private void Awake()
    {
        capsuleControllerActions = new CapsuleController();
        capsuleRigidBody = GetComponent<Rigidbody>();

        capsuleControllerActions.Capsule.Jump.performed += Jump;
        capsuleControllerActions.Capsule.Jump.canceled += JumpNo;
        capsuleControllerActions.Capsule.Forward.performed += Forward;
        capsuleControllerActions.Capsule.Forward.canceled += ForwardNo;
        capsuleControllerActions.Capsule.Backward.performed += Backward;
        capsuleControllerActions.Capsule.Backward.canceled += BackwardNo;
        capsuleControllerActions.Capsule.Right.performed += Right;
        capsuleControllerActions.Capsule.Right.canceled += RightNo;
        capsuleControllerActions.Capsule.Left.performed += Left;
        capsuleControllerActions.Capsule.Left.canceled += LeftNo;

        capsuleControllerActions.Capsule.Enable();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 movement = Vector3.zero;

        if (isForwardPressed)
        {
            movement += new Vector3(0, 0, 1f) * movementSpeed;
        }

        if (isBackwardPressed)
        {
            movement += new Vector3(0, 0, -1f) * movementSpeed;
        }

        if (isRightPressed)
        {
            movement += new Vector3(1f, 0, 0) * movementSpeed;
        }

        if (isLeftPressed)
        {
            movement += new Vector3(-1f, 0, 0) * movementSpeed;
        }

        capsuleRigidBody.AddForce(movement, ForceMode.VelocityChange);

        if (isJumping)
        {
         if(IsGrounded())
           { capsuleRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isJumping = false; }// Reset jump state
        }
    }

    #region CAPSULE_MOVEMENT
    private void Jump(InputAction.CallbackContext context)
    {
         isJumping = true;
    }
    private void JumpNo(InputAction.CallbackContext context)
    {
        // Nothing specific to do when jump is released
    }
    private void Forward(InputAction.CallbackContext context)
    {
        isForwardPressed = true;
    }
    private void ForwardNo(InputAction.CallbackContext context)
    {
        isForwardPressed = false;
    }

    private void Backward(InputAction.CallbackContext context)
    {
        isBackwardPressed = true;
    }

    private void BackwardNo(InputAction.CallbackContext context)
    {
        isBackwardPressed = false;
    }

    private void Right(InputAction.CallbackContext context)
    {
        isRightPressed = true;
    }

    private void RightNo(InputAction.CallbackContext context)
    {
        isRightPressed = false;
    }

    private void Left(InputAction.CallbackContext context)
    {
        isLeftPressed = true;
    }

    private void LeftNo(InputAction.CallbackContext context)
    {
        isLeftPressed = false;
    }
    #endregion

    
    bool IsGrounded()
    {
      return Physics.CheckSphere(groundCheck.position , .1f , ground);
    }

    public void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.name=="RestGame")
      {
        SceneManager.LoadScene("Scene1");
        Debug.Log("Player Is Lost!");
      }
    }
}
