using UnityEngine;

public class Tail : MonoBehaviour
{
    // Other components
    private Snake snake;
    private Transform target;

    public void Setup(Snake snake, Transform target)
    {
        this.snake = snake;
        this.target = target;

        transform.position = target.position - snake.MoveDirection;
    }

    private void Update()
    {
        transform.position = target.position - snake.MoveDirection;
    }
}
