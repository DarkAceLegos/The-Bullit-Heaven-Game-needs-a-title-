using System;
using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;

    private void FixedUpdate()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        var ts = TimeSpan.FromSeconds(double.Parse(Time.timeSinceLevelLoad.ToString()));
        timeText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }
}
