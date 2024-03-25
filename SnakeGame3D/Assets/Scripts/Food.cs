using UnityEngine;

public class Food : MonoBehaviour
{
    public void MoveRandom(Vector3 exceptPosition)
    {
        transform.position = new Vector3(Random.Range(-10, 11), Random.Range(-4, 5), Random.Range(-8, 9));

        if ((exceptPosition - transform.position).sqrMagnitude <= 4f)
        {
            MoveRandom(exceptPosition);
        }
    }
}
