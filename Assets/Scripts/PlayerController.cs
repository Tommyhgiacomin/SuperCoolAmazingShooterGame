using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IDamageable
{
    protected Health health;
    public bool IsDead => health.IsDead;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float gravity = -20f;
    public float runSpeed = 10f;

    private bool _isRunning = false;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;
    public Transform cameraHolder;

    float xRotation;
    float yVelocity;
    CharacterController controller;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameActive) return;

        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isRunning = true;
            AudioManager.Instance.rtpcPlayerSpeed.SetGlobalValue(2);
        }
        else
        {
            _isRunning = false;
            AudioManager.Instance.rtpcPlayerSpeed.SetGlobalValue(1);
        }

        float speed = _isRunning ? runSpeed : moveSpeed;

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded && yVelocity < 0f)
            yVelocity = -2f;

        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity;

        controller.Move(move * speed * Time.deltaTime);
    }
    public void TakeDamage(int amount)
    {
    }

    private void Die()
    {
    }

}
