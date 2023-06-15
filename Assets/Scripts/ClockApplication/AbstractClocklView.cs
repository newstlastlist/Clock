using System;
using TMPro;
using UnityEngine;

public abstract class AbstractClocklView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeTmpro;

    public virtual void DisplayTimeDateTime(DateTime time)
    {
        FormatAndDisplay(time.Hour, time.Minute, time.Second);
    }

    public virtual void DisplayTimeTimeSpan(TimeSpan time)
    {
        FormatAndDisplay(time.Hours, time.Minutes, time.Seconds);
    }

    private void FormatAndDisplay(int hours, int minutes, int seconds)
    {
        var hour = Utils.FormatDigit(hours);
        var min = Utils.FormatDigit(minutes);
        var sec = Utils.FormatDigit(seconds);
        
        _timeTmpro.text = String.Format("{0}:{1}:{2}", hour, min, sec);
    }
}
