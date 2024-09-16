using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveInput;
    InputAction interactInput;
    InputAction interactDirInput;
    InputAction jumpInput;
    Rigidbody2D rigidbody;
    public Camera camera;
    public bool canJump = false;
    public bool doubleJump = false;
    public float speed = 5.0f;
    bool freezTime = false;
    InteractiveData interactiveData;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput)
        {
            moveInput = playerInput.actions.FindAction("Move");
            interactInput = playerInput.actions.FindAction("Interact");
            interactDirInput = playerInput.actions.FindAction("InteractDir");
            jumpInput = playerInput.actions.FindAction("Jump");
        }
        rigidbody = GetComponent<Rigidbody2D>();
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
            rigidbody.AddForce(new Vector2(0, 200));
            if (canJump)
                canJump = false;
            else
                doubleJump = false;


        }
        if (interactInput.WasPressedThisFrame())
        {
            ActionAfterFreez();
            Time.timeScale = 1.0f;
            freezTime = false;
        }
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


        switch (interactiveData.type)
        {
            case InteractiveType.Throw:
                Vector2 useDir = worldPosition - (Vector2)toThrow.transform.position;
                Rigidbody2D rigidbody = toThrow.GetComponent<Rigidbody2D>();
                if (!rigidbody)
                    break;
                rigidbody.velocity = Vector2.zero;
                rigidbody.angularVelocity = 0;
                rigidbody.AddForce(useDir.normalized * interactiveData.power);
                break;

            default:
                break;
        }
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
        interactiveData = interactive.interactiveData;
        transform.position = collision.transform.position;
        
        Destroy(collision.gameObject);
        Time.timeScale = 0.0f;
        freezTime = true;
    }
}
