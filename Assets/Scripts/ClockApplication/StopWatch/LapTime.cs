using System;
using TMPro;
using UnityEngine;

public class LapTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;

    public void SetTimeText(TimeSpan time)
    {
        FormatAndDisplay(time.Days, time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
    }
    private void FormatAndDisplay(int days, int hours, int minutes, int seconds, int millisec)
    {
        var hour = Utils.FormatDigit(hours);
        var min = Utils.FormatDigit(minutes);
        var sec = Utils.FormatDigit(seconds);
        var mills = Utils.FormatDigit(millisec);
        
        if(days < 1)
            _timeText.text = String.Format("{0}:{1}:{2}:{3}", hour, min, sec, mills);
        else
            _timeText.text = String.Format("d{0}:{1}:{2}:{3}:{4}", days, hour, min, sec, mills);
    }
}
