using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D rigidBody = null;
    Trajectory trajectory = null;
    Camera mainCamera;
    Animator animator= null;
    SpriteRenderer spriteRenderer = null;
    public WallJumpTrigger leftWallTrigger = null;
    public WallJumpTrigger rightWallTrigger = null;

    InputAction moveInput;
    InputAction interactInput;
    InputAction acceptInput;
    InputAction interactDirInput;
    InputAction jumpInput;

    public bool canJump = false;
    public bool doubleJump = false;
    public float speed = 5.0f;
    public float jumpForce = 3.0f;

    Vector2 throwDir = Vector2.zero;
    bool freezeTime = false;
    InteractiveData interactiveData;


    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        trajectory = GetComponentInChildren<Trajectory>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        if (playerInput)
        {
            moveInput = playerInput.actions.FindAction("Move");
            interactInput = playerInput.actions.FindAction("Interact");
            acceptInput = playerInput.actions.FindAction("Accept");
            interactDirInput = playerInput.actions.FindAction("InteractDir");
            jumpInput = playerInput.actions.FindAction("Jump");
        }

    }

    void Update()
    {
        Move();
        HandleJump();
        HandleInteractInput();
        HandleAcceptInput();

        if (freezeTime && trajectory)
        {
            HandleFreezeTime();
        }
    }

    private void Move()
    {
        float moveX = 0.0f;
        if (moveInput != null)
            moveX = moveInput.ReadValue<float>();
        if (moveX != 0.0f) 
        {
            spriteRenderer.flipX = moveX <= 0.0f;

            transform.position += new Vector3(moveX * speed * Time.deltaTime, 0, 0);
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

    }

    void HandleJump()
    {
        if (!jumpInput.WasPressedThisFrame())
            return;
        if (leftWallTrigger.stickToTheWall && canJump) 
        {
            rigidBody.AddForce(new Vector2(1, 1).normalized * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }
        else if (rightWallTrigger.stickToTheWall && canJump)
        {
            rigidBody.AddForce(new Vector2(-1, 1).normalized * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }
        else if (canJump || doubleJump)
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0;
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if (canJump)
            {
                canJump = false;
                animator.SetBool("Jump", true);
            }
            else
            {
                doubleJump = false;
                animator.SetBool("DoubleJump", true);
            }


        }

    }

    void HandleInteractInput()
    {
        if (interactInput.WasPressedThisFrame())
        {
            GameObject gm = GetPressObject();
            if (gm)
                interactiveData.toThrow = gm;
        }
    }

    void HandleAcceptInput()
    {
        if (acceptInput.WasPressedThisFrame())
        {
            ActionAfterFreeze();
            UnfreezeTime();
        }
    }

    void UnfreezeTime()
    {
        Time.timeScale = 1.0f;
        freezeTime = false;
        trajectory.ShowDots(false);
    }

    void HandleFreezeTime()
    {
        GameObject toThrow = GetObjectToThrow();
        Vector2 throwDir = GetThrowDir();

        SetTrajectory(toThrow, throwDir);
        trajectory.ShowDots(true);
    }

    GameObject GetObjectToThrow()
    {
        return interactiveData.toThrow != null ? interactiveData.toThrow : gameObject;
    }
    void SetTrajectory(GameObject toThrow, Vector2 direction)
    {
        trajectory.dir = direction;
        trajectory.forcePower = interactiveData.power / GetPowerDivider(interactiveData.type);
        trajectory.mass = GetThrowMass();

        if (interactiveData.type == InteractiveType.Throw)
        {
            trajectory.UpdateThrowDotPos(toThrow);
        }
        else if (interactiveData.type == InteractiveType.MovePlatform)
        {
            trajectory.UpdatePlatformDotPos(toThrow, interactiveData.timer);
        }
    }

    float GetThrowMass()
    {
        if (interactiveData.toThrow)
        {
            return interactiveData.toThrow.GetComponent<Rigidbody2D>().mass;
        }
        return rigidBody.mass;
    }
    void ActionAfterFreeze()
    {
        if (!freezeTime)
            return;

        Vector3 mousePos = interactDirInput.ReadValue<Vector2>();
        mousePos.z = mainCamera.nearClipPlane;
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);

        float power = interactiveData.power;
        GameObject toThrow = interactiveData.toThrow;
        if (toThrow == null)
            toThrow = gameObject;
        Vector2 useDir = worldPosition - (Vector2)toThrow.transform.position;

        switch (interactiveData.type)
        {
            case InteractiveType.Throw:
                ThrowBody tb = toThrow.GetComponent<ThrowBody>();
                if (tb)
                    tb.Throw(useDir.normalized, power, interactiveData.timer);
                break;
            case InteractiveType.MovePlatform:
                MovingPlatform mp = toThrow.GetComponent<MovingPlatform>();
                if (mp)
                    mp.Move(useDir.normalized, power, interactiveData.timer);
                break;
            default:
                break;
        }
    }
    Vector2 GetThrowDir()
    {
        Vector3 mousePos = interactDirInput.ReadValue<Vector2>();
        mousePos.z = mainCamera.nearClipPlane;
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        _ = interactiveData.power;
        GameObject toThrow = interactiveData.toThrow;
        if (toThrow == null)
            toThrow = gameObject;


        switch (interactiveData.type)
        {
            case InteractiveType.Throw:
            case InteractiveType.MovePlatform:
                return (worldPosition - (Vector2)toThrow.transform.position).normalized;
            default:
                break;
        }
        return Vector2.zero;
    }
    GameObject GetPressObject()
    {
        Vector3 mousePos = interactDirInput.ReadValue<Vector2>();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        Collider2D col = Physics2D.OverlapPoint(worldPos);
        if (!col)
            return null;
        GameObject gm = col.gameObject;
        if (!gm)
            return null;

        switch (interactiveData.type)
        {
            case InteractiveType.Throw:
                ThrowBody tb = gm.GetComponent<ThrowBody>();
                if (tb)
                    return tb.gameObject;
                break;
            case InteractiveType.MovePlatform:
                MovingPlatform mp = gm.GetComponent<MovingPlatform>();
                if (mp)
                    return mp.gameObject;
                break;

        }
        return null;
    }

    float GetPowerDivider(InteractiveType type)
    {
        return type == InteractiveType.Throw ? 3f : 1f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
            doubleJump = true;
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactive interactive = collision.gameObject.GetComponent<Interactive>();
        if (!interactive)
            return;
        if (!interactive.CanInteract())
            return;
        interactiveData = interactive.GetData();
        interactive.Interact();
        //transform.position = collision.transform.position;
       
        Time.timeScale = 0.0f;
        freezeTime = true;
    }

    public void resetJump()
    {
        canJump = true;
        doubleJump = true;
    }
}
