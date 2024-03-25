using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sudoku3D : MonoBehaviour
{
    [SerializeField]
    private NumberText numberTextPrefab;
    [SerializeField]
    private TextMeshProUGUI problemText;

    private List<NumberText> board = new List<NumberText>();

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                NumberText instance = Instantiate(numberTextPrefab, new Vector3(x * 1.5f - 6f, y * 1.25f - 5f, 0f), Quaternion.identity, transform);

                instance.Setup(x * 9 + y);

                board.Add(instance);
            }
        }
    }

    /// <summary>
    /// Write a number in specific position.
    /// </summary>
    /// <param name="index">The position of number.</param>
    /// <returns>Returns true if there are no problems. Otherwise returns false.</returns>
    public bool Write(int index, int value)
    {
        board[index].Value = value;

        if (!CheckRow())
        {
            return false;
        }

        if (!ChecColumn())
        {
            return false;
        }

        if (!CheckBox())
        {
            return false;
        }

        return true;
    }

    public void VisibleProblemText(bool visible)
    {
        problemText.enabled = visible;
    }

    private bool CheckRow()
    {
        Dictionary<int, bool> temp = new Dictionary<int, bool>();

        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (!temp.TryAdd(board[x * 9 + y].Value, true))
                {
                    return false;
                }
            }

            temp.Clear();
        }

        return true;
    }

    private bool ChecColumn()
    {
        Dictionary<int, bool> temp = new Dictionary<int, bool>();

        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (!temp.TryAdd(board[x * 9 + y].Value, true))
                {
                    return false;
                }
            }

            temp.Clear();
        }

        return true;
    }

    private bool CheckBox()
    {
        Dictionary<int, bool> temp = new Dictionary<int, bool>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (!temp.TryAdd(board[(x + i * 3) * 9 + y + j * 3].Value, true))
                        {
                            return false;
                        }
                    }
                }

                temp.Clear();
            }
        }

        return true;
    }
}
