using TMPro;
using UnityEngine;

public class NumberText : MonoBehaviour
{
    private TextMeshPro text;

    private int value;
    public int Value
    {
        get => value;
        set
        {
            this.value = value;

            text.text = $"{value}";
        }
    }

    public int Index { get; private set; }

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    public void Setup(int index)
    {
        Index = index;
    }
}
