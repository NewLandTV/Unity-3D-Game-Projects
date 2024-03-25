using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float limitRotateX;

    [SerializeField]
    private LayerMask numberTextLayerMask;

    [SerializeField]
    private Sudoku3D sudoku3D;

    private float currentXRotation;
    private float currentYRotation;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        Rotate();
        WriteNumber();
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

        currentXRotation -= x;
        currentYRotation += y;

        currentXRotation = Mathf.Clamp(currentXRotation, -limitRotateX, limitRotateX);

        transform.localEulerAngles = new Vector3(currentXRotation, currentYRotation, 0f);
    }

    private void WriteNumber()
    {
        if (int.TryParse(Input.inputString, out int value))
        {
            if (!Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hit, Mathf.Infinity, numberTextLayerMask.value))
            {
                return;
            }

            bool noProblem = sudoku3D.Write(hit.collider.GetComponent<NumberText>().Index, value);

            sudoku3D.VisibleProblemText(!noProblem);
        }
    }
}
