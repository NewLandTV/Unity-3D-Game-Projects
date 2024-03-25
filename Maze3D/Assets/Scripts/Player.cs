using UnityEngine;

public class Player : MonoBehaviour
{
    // Move speed
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float currentSpeed;

    private Vector3 moveDirection;

    // Character and Camera rotation
    [SerializeField]
    private float rotationSensitivity;
    [SerializeField]
    private float limitCameraRotationX;

    private Vector2 rotation;

    // Jump and Gravity scale
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravityScale;

    // Flags
    private bool isRun;
    private bool isJump;

    // Character components
    [HideInInspector]
    public Transform transform;
    private Rigidbody rigidbody;
    private CapsuleCollider collider;

    // Other components
    private Transform camera;
    [SerializeField]
    private MazeGenerator mazeGenerator;

    [SerializeField]
    private Vector2Int mazeSize;

    private void Awake()
    {
        // Get components
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();

        // Get other components
        camera = Camera.main.GetComponent<Transform>();

        currentSpeed = walkSpeed;
    }

    private void Start()
    {
        mazeGenerator.Generate((ushort)mazeSize.x, (ushort)mazeSize.y);
    }

    private void Update()
    {
        ToggleShowCursor();
        TryRun();
        CameraRotation();
        CharacterRotation();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void ToggleShowCursor()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isRun = true;
            currentSpeed = runSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isRun = false;
            currentSpeed = walkSpeed;
        }
    }

    private void CameraRotation()
    {
        rotation.x -= Input.GetAxisRaw("Mouse Y") * rotationSensitivity;

        rotation.x = Mathf.Clamp(rotation.x, -limitCameraRotationX, limitCameraRotationX);

        camera.rotation = Quaternion.Euler(new Vector3(rotation.x, camera.eulerAngles.y, camera.eulerAngles.z));
    }

    private void CharacterRotation()
    {
        rotation.y += Input.GetAxisRaw("Mouse X") * rotationSensitivity;

        transform.rotation = Quaternion.Euler(transform.up * rotation.y);
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = (transform.right * horizontal + transform.forward * vertical).normalized * currentSpeed * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + moveDirection);
    }

    private void Jump()
    {
        if (rigidbody.velocity.y < 0f && Physics.SphereCast(transform.position, collider.radius * 0.95f, Vector3.down, out RaycastHit hit, collider.height * 0.28f) && !hit.collider.CompareTag("Character"))
        {
            isJump = false;
        }
        else if (rigidbody.velocity.y > 0f)
        {
            rigidbody.AddForce(Vector3.down * gravityScale * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        if (!Input.GetKeyDown(KeyCode.Space) || isJump)
        {
            return;
        }

        isJump = true;

        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
