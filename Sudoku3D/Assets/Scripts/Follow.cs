using UnityEngine;

public class Follow : MonoBehaviour
{
    private new Transform transform;

    [SerializeField]
    private Transform target;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
        }
    }
}
