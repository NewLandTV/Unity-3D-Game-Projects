using UnityEngine;

public enum Direction
{
    Left,
    Up,
    Right,
    Down
}

public enum MazeElement
{
    Wall,
    Empty,
    Visited,
    Goal
}

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;
    [SerializeField]
    private GameObject goalPrefab;
    [SerializeField]
    private Transform floor;

    // Constants
    private readonly int[,] DIRECTION = new int[4, 2]
    {
        { 0, -2 },
        { 0, 2 },
        { -2, 0 },
        { 2, 0 }
    };

    // Map
    private MazeElement[,] map;

    private ushort width;
    private ushort depth;

    public void Generate(ushort width, ushort depth)
    {
        floor.position = new Vector3(width >> 1, 0f, depth >> 1);
        floor.localScale = new Vector3(width, 1f, depth);

        map = new MazeElement[width, depth];

        this.width = width;
        this.depth = depth;

        Vector2Int startPoint = new Vector2Int(Random.Range(0, width), Random.Range(0, depth));

        RandomMap(startPoint.x, startPoint.y);

        map[width - 2, depth - 2] = MazeElement.Goal;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if (map[x, z] == MazeElement.Wall)
                {
                    MakeCube(new Vector3(x, 2.625f, z));
                }
                else if (map[x, z] == MazeElement.Goal)
                {
                    Instantiate(goalPrefab, new Vector3(x, 1f, z), Quaternion.identity, transform);
                }
            }
        }
    }

    private void ShuffleArray(ref Direction[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int random = Random.Range(0, array.Length - i) + i;
            Direction temp = array[i];

            array[i] = array[random];
            array[random] = temp;
        }
    }

    private bool CheckMazeRange(int x, int z)
    {
        return x > 0 && x < width - 1 && z > 0 && z < depth - 1;
    }

    private void RandomMap(int x, int z)
    {
        Direction[] directions = new Direction[4]
        {
            Direction.Up,
            Direction.Right,
            Direction.Down,
            Direction.Left
        };

        map[x, z] = MazeElement.Visited;

        ShuffleArray(ref directions);

        for (int i = 0; i < 4; i++)
        {
            int nx = DIRECTION[(int)directions[i], 0] + x;
            int nz = DIRECTION[(int)directions[i], 1] + z;

            if (CheckMazeRange(nx, nz) && map[nx, nz] == MazeElement.Wall)
            {
                RandomMap(nx, nz);

                if (z != nz)
                {
                    map[nx, z + nz >> 1] = MazeElement.Empty;
                }
                else
                {
                    map[x + nx >> 1, nz] = MazeElement.Empty;
                }

                map[nx, nz] = MazeElement.Empty;
            }
        }
    }

    private void MakeCube(Vector3 position)
    {
        GameObject instance = Instantiate(cubePrefab, position, Quaternion.identity, transform);
    }
}
