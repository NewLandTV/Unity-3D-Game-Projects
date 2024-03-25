using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Move
    [Header("Move"), SerializeField]
    private float moveSpeed;

    // Rotate
    [Space, Header("Rotate"), SerializeField]
    private float rotateSensitivity;
    [SerializeField]
    private float limitRotateX;

    private Vector2 currentRotation;

    // Flags
    [Space, Header("Flags"), SerializeField]
    private bool hideCursor;
    private bool xTurn;

    // Components
    [HideInInspector]
    public new Transform transform;

    // Other components
    [Space, Header("Other Components"), SerializeField]
    private Board board;

    private void Awake()
    {
        // Get components
        transform = GetComponent<Transform>();

        // Settings
        if (hideCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Initialize
        board.Initialize();
    }

    private void Update()
    {
        Move();
        Rotate();
        SignInBoard();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.position += (transform.right * x + transform.forward * z).normalized * moveSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        float x = Input.GetAxisRaw("Mouse Y");
        float y = Input.GetAxisRaw("Mouse X");

        currentRotation.x -= x * rotateSensitivity;
        currentRotation.y += y * rotateSensitivity;

        currentRotation.x = Mathf.Clamp(currentRotation.x, -limitRotateX, limitRotateX);

        transform.eulerAngles = currentRotation;
    }

    private void SignInBoard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            uint x = (uint)Mathf.FloorToInt(transform.position.x);
            uint y = (uint)Mathf.FloorToInt(transform.position.y);
            uint z = (uint)Mathf.FloorToInt(transform.position.z);

            xTurn = board.SignBoard(x, y, z, xTurn ? Sign.X : Sign.O) ? !xTurn : xTurn;
        }
    }
}
