using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour,ForceBodyI
{
    PlayerInput playerInput;
    InputAction moveInput;
    InputAction interactInput;
    InputAction acceptInput;
    InputAction interactDirInput;
    InputAction jumpInput;
    Rigidbody2D rigidbody;
    Trajectory trajectory;
    public Camera camera;
    public bool canJump = false;
    public bool doubleJump = false;
    public float speed = 5.0f;
    Vector2 throwDir = Vector2.zero;
    bool freezTime = false;
    InteractiveData interactiveData;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput)
        {
            moveInput = playerInput.actions.FindAction("Move");
            interactInput = playerInput.actions.FindAction("Interact");
            acceptInput = playerInput.actions.FindAction("Accept");
            interactDirInput = playerInput.actions.FindAction("InteractDir");
            jumpInput = playerInput.actions.FindAction("Jump");
        }
        rigidbody = GetComponent<Rigidbody2D>();
        trajectory = GetComponentInChildren<Trajectory>();
        //camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if ((canJump || doubleJump) && jumpInput.WasPressedThisFrame())
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0;
            rigidbody.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            if (canJump)
                canJump = false;
            else
                doubleJump = false;


        }
        if (interactInput.WasPressedThisFrame())
        {
            GameObject gm = getPressObject();
            if (gm)
                interactiveData.toThrow = gm;
        }
        if (acceptInput.WasPressedThisFrame())
        {
            ActionAfterFreez();
            Time.timeScale = 1.0f;
            freezTime = false;
            trajectory.showDots(false);
        }
        if (freezTime && trajectory) 
        {
            GameObject toThrow = interactiveData.toThrow;
            
            if (toThrow == null)
                toThrow = gameObject;
            trajectory.dir = getThrowDir();

            switch (interactiveData.type)
            {
                case InteractiveType.Throw:
                    trajectory.forcePower = interactiveData.power / 3;
                    trajectory.updateThrowDotPos(toThrow);
                    break;
                case InteractiveType.MovePlatrorm:
                    trajectory.forcePower = interactiveData.power;
                    trajectory.updatePlatformDotPos(toThrow, interactiveData.timer);
                    break;

            }

            trajectory.mass = getThrowMass();
            trajectory.showDots(true);

        }
    }

    float getThrowMass()
    {
        if(interactiveData.toThrow)
        {
            return interactiveData.toThrow.GetComponent<Rigidbody2D>().mass;
        }
        return rigidbody.mass;
    }
    void ActionAfterFreez()
    {
        if (!freezTime)
            return;

        Vector3 mousePos = interactDirInput.ReadValue<Vector2>();
        mousePos.z = camera.nearClipPlane;
        Vector2 worldPosition = camera.ScreenToWorldPoint(mousePos);

        float power = interactiveData.power;
        GameObject toThrow = interactiveData.toThrow;
        if (toThrow == null)
            toThrow = gameObject;
        Vector2 useDir = worldPosition - (Vector2)toThrow.transform.position;

        switch (interactiveData.type)
        {
            case InteractiveType.Throw:
                ForceBodyI fbi = toThrow.GetComponent<ForceBodyI>();
                if (fbi == null)
                    return;
                fbi.addForce(useDir.normalized, power,interactiveData.timer);
                break;
            case InteractiveType.MovePlatrorm:
                PlatformI pi = toThrow.GetComponent<PlatformI>();
                if (pi == null)
                    return;
                pi.moveTo(useDir.normalized, power,interactiveData.timer);
                break;
            default:
                break;
        }
    }
    Vector2 getThrowDir()
    {
        Vector3 mousePos = interactDirInput.ReadValue<Vector2>();
        mousePos.z = camera.nearClipPlane;
        Vector2 worldPosition = camera.ScreenToWorldPoint(mousePos);

        float power = interactiveData.power;
        GameObject toThrow = interactiveData.toThrow;
        if (toThrow == null)
            toThrow = gameObject;


        switch (interactiveData.type)
        {
            case InteractiveType.Throw:
            case InteractiveType.MovePlatrorm:
                return (worldPosition - (Vector2)toThrow.transform.position).normalized;
            default:
                break;
        }
        return Vector2.zero;
    }
    GameObject getPressObject()
    {
        Vector3 mousePos = interactDirInput.ReadValue<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePos);
        Collider2D col = Physics2D.OverlapPoint(worldPos);
        if (!col)
            return null;
        GameObject gm = col.gameObject;
        if (!gm)
            return null;

        switch (interactiveData.type)
        {
            case InteractiveType.Throw:
                ForceBodyI fbi = gm.GetComponent<ForceBodyI>();
                if (fbi != null)
                    return gm;
                break;
            case InteractiveType.MovePlatrorm:
                PlatformI pi = gm.GetComponent<PlatformI>();
                if (pi != null)
                    return gm;
                break;

        }

        return null;

    }
    void Move()
    {
        float moveX = 0.0f;
        if (moveInput != null)
            moveX = moveInput.ReadValue<float>();
        transform.position += new Vector3(moveX * speed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
            doubleJump = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactive interactive = collision.gameObject.GetComponent<Interactive>();
        if (!interactive)
            return;
        interactiveData = interactive.getData();
        //transform.position = collision.transform.position;
        
        Destroy(collision.gameObject);
        Time.timeScale = 0.0f;
        freezTime = true;
    }

    public void addForce(Vector2 dir, float power, float timer)
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        rigidbody.AddForce(dir * power, ForceMode2D.Impulse);
    }
}
