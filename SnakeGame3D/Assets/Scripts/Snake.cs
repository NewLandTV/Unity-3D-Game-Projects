using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Move
    [Header("Move"), SerializeField]
    private float speed;
    public float Speed => speed;

    private Vector3 moveDirection = Vector3.forward;
    public Vector3 MoveDirection => moveDirection;

    // Prefabs
    [Space, Header("Prefabs"), SerializeField]
    private Tail tailPrefab;

    // Other components
    [Space, Header("Other components"), SerializeField]
    private Food food;

    // Flags
    private bool isGameOver;

    // Datas
    private List<Tail> tails = new List<Tail>();

    private void Update()
    {
        if (isGameOver)
        {
            return;
        }

        Move();
    }

    private void OnTriggerStay(Collider other)
    {
        if (isGameOver)
        {
            return;
        }

        if (other.CompareTag("Wall"))
        {
            GameOver();
        }
        if (other.CompareTag("Food"))
        {
            EatFood();
        }
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W) && moveDirection != Vector3.back)
        {
            moveDirection = Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.A) && moveDirection != Vector3.right)
        {
            moveDirection = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.S) && moveDirection != Vector3.forward)
        {
            moveDirection = Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.D) && moveDirection != Vector3.left)
        {
            moveDirection = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.Q) && moveDirection != Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.E) && moveDirection != Vector3.up)
        {
            moveDirection = Vector3.down;
        }

        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void GameOver()
    {
        isGameOver = true;
    }

    private void EatFood()
    {
        food.MoveRandom(transform.position);

        Tail tail = Instantiate(tailPrefab, transform.position - moveDirection, Quaternion.identity);

        tail.Setup(this, tails.Count <= 0 ? transform : tails[tails.Count - 1].transform);

        tails.Add(tail);
    }
}
