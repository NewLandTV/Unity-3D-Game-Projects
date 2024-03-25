using System;
using TMPro;
using UnityEngine;

// Enums
public enum Sign
{
    None,
    O,
    X
}

public class Board : MonoBehaviour
{
    // Prefabs
    [Header("Prefabs"), SerializeField]
    private GameObject signPrefab;

    // Datas
    private Sign[] board = new Sign[27];

    // Other components
    [Space, Header("Other Components"), SerializeField]
    private WinnerText winnerText;

    public void Initialize()
    {
        for (uint i = 0; i < board.Length; i++)
        {
            board[i] = Sign.None;
        }
    }

    public bool SignBoard(uint x, uint y, uint z, Sign sign)
    {
        if (x < 0 || x >= 3 || y < 0 || y >= 3 || z < 0 || z >= 3 || board[x + y * 9 + z * 3] != Sign.None)
        {
            return false;
        }

        board[x + y * 9 + z * 3] = sign;

        MakeSignObject(x, y, z, sign);
        CheckWinner(sign);

        return true;
    }

    private void MakeSignObject(uint x, uint y, uint z, Sign sign)
    {
        GameObject instance = Instantiate(signPrefab, new Vector3(x, y, z), Quaternion.identity, transform);

        instance.transform.localPosition = new Vector3(x, y, z);

        instance.GetComponent<TextMeshPro>().text = $"{(sign == Sign.None ? string.Empty : Enum.GetName(typeof(Sign), sign))}";
    }

    private void CheckWinner(Sign sign)
    {
        if ((board[0] == sign && board[1] == sign && board[2] == sign) || (board[3] == sign && board[4] == sign && board[5] == sign) || (board[6] == sign && board[7] == sign && board[8] == sign) || (board[9] == sign && board[10] == sign && board[11] == sign) || (board[12] == sign && board[13] == sign && board[14] == sign) || (board[15] == sign && board[16] == sign && board[17] == sign) || (board[18] == sign && board[19] == sign && board[20] == sign) || (board[21] == sign && board[22] == sign && board[23] == sign) || (board[24] == sign && board[25] == sign && board[26] == sign) ||
            (board[0] == sign && board[3] == sign && board[6] == sign) || (board[1] == sign && board[4] == sign && board[7] == sign) || (board[2] == sign && board[5] == sign && board[8] == sign) || (board[9] == sign && board[12] == sign && board[15] == sign) || (board[10] == sign && board[13] == sign && board[16] == sign) || (board[11] == sign && board[14] == sign && board[17] == sign) || (board[18] == sign && board[21] == sign && board[24] == sign) || (board[19] == sign && board[22] == sign && board[25] == sign) || (board[20] == sign && board[23] == sign && board[26] == sign) ||
            (board[0] == sign && board[9] == sign && board[18] == sign) || (board[1] == sign && board[10] == sign && board[19] == sign) || (board[2] == sign && board[11] == sign && board[20] == sign) || (board[3] == sign && board[12] == sign && board[21] == sign) || (board[4] == sign && board[13] == sign && board[22] == sign) || (board[5] == sign && board[14] == sign && board[23] == sign) || (board[6] == sign && board[15] == sign && board[24] == sign) || (board[7] == sign && board[16] == sign && board[25] == sign) || (board[8] == sign && board[17] == sign && board[26] == sign) ||
            (board[0] == sign && board[10] == sign && board[20] == sign) || (board[2] == sign && board[10] == sign && board[18] == sign) || (board[3] == sign && board[13] == sign && board[23] == sign) || (board[5] == sign && board[13] == sign && board[21] == sign) || (board[6] == sign && board[16] == sign && board[26] == sign) || (board[8] == sign && board[16] == sign && board[24] == sign) ||
            (board[2] == sign && board[14] == sign && board[26] == sign) || (board[8] == sign && board[14] == sign && board[20] == sign) || (board[1] == sign && board[13] == sign && board[25] == sign) || (board[7] == sign && board[13] == sign && board[19] == sign) || (board[0] == sign && board[12] == sign && board[24] == sign) || (board[6] == sign && board[12] == sign && board[18] == sign) ||
            (board[0] == sign && board[4] == sign && board[8] == sign) || (board[2] == sign && board[4] == sign && board[6] == sign) || (board[9] == sign && board[13] == sign && board[17] == sign) || (board[11] == sign && board[13] == sign && board[15] == sign) || (board[18] == sign && board[22] == sign && board[26] == sign) || (board[20] == sign && board[22] == sign && board[24] == sign) ||
            (board[0] == sign && board[13] == sign && board[26] == sign) || (board[2] == sign && board[13] == sign && board[24] == sign) || (board[8] == sign && board[13] == sign && board[18] == sign) || (board[6] == sign && board[13] == sign && board[20] == sign))
        {
            winnerText.Set(sign);
        }
    }
}
