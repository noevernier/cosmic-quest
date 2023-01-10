using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    float speed = 5.0f;
    float runMultiplier = 3.0f;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 7.0f;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharacterControls.Move.started += onMouvementInput;
        playerInput.CharacterControls.Move.canceled += onMouvementInput;
        playerInput.CharacterControls.Move.performed += onMouvementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if(isMovementPressed) {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame*Time.deltaTime);
        }
    }

    void onMouvementInput(InputAction.CallbackContext context)
    {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x * speed;
            currentMovement.z = currentMovementInput.y * speed;

            currentRunMovement.x = currentMovementInput.x * speed * runMultiplier;
            currentRunMovement.z = currentMovementInput.y * speed * runMultiplier;

            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);;

        if(isMovementPressed && !isWalking){
            animator.SetBool(isWalkingHash, true);
        } else if(!isMovementPressed && isWalking) {
            animator.SetBool(isWalkingHash, false);
        }

        if(isMovementPressed && isRunPressed && !isRunning){
            animator.SetBool(isRunningHash, true);
        } else if((!isMovementPressed || !isRunPressed) && isRunning) {
            animator.SetBool(isRunningHash, false);
        }



    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();

        if(isRunPressed){
            characterController.Move(currentRunMovement * Time.deltaTime);
        } else {
            characterController.Move(currentMovement* Time.deltaTime);
        }
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
