using System;
using TMPro;
using UnityEngine;

public class WinnerText : MonoBehaviour
{
    // Components
    private TextMeshProUGUI text;

    private void Awake()
    {
        // Get components
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Set(Sign sign)
    {
        text.text = $"{Enum.GetName(typeof(Sign), sign)} Won!";
    }
}
