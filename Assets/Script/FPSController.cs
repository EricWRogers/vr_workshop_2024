using UnityEngine.InputSystem;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkspeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 10f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXlimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public bool canMove = true;
    public bool cursorVisible = false;
    public bool playerFrozen = false;

    //Timers for footstep sound
    public float footstepSoundDelay = 1.0f;
    private float footstepTimer = 0.0f;
    public int footNum = 1; 

    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        if (cursorVisible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerFrozen)
        {
            Vector3 forward = playerCamera.transform.forward;
            Vector3 right = playerCamera.transform.right;

            bool isRunning = Keyboard.current.leftShiftKey.isPressed;
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkspeed) * (Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue()) : 0f;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkspeed) * (Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue()) : 0f;
            float movementDirectionY = moveDirection.y;
            moveDirection = forward * curSpeedX + right * curSpeedY;

            if (Keyboard.current.spaceKey.wasPressedThisFrame && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Mouse.current.delta.y.ReadValue() * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXlimit, lookXlimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Mouse.current.delta.x.ReadValue() * lookSpeed, 0);
            }

            //Foostep sound stuff
            footstepTimer -= Time.deltaTime;
            if (curSpeedX != 0f || curSpeedY != 0f)
            {
                if (footstepTimer < 0.2)
                {
                    if (footNum == 1)
                    {
                        AudioManager.instance.Play("Stone_Step_1");
                        footstepTimer = footstepSoundDelay;
                        footNum++;
                    }
                    else if (footNum == 2)
                    {
                        AudioManager.instance.Play("Stone_Step_2");
                        footstepTimer = footstepSoundDelay;
                        footNum--;
                    }
                }
            }
        }
    }

    public void FreezePlayer()
    {
        playerFrozen = true;
    }

    public void UnfreezePlayer()
    {
        playerFrozen = false;
    }
}