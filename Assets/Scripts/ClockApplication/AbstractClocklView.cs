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
    public virtual void DisplayTimeTimeSpanWithMillisecondsAndDays(TimeSpan time)
    {
        FormatAndDisplay(time.Days, time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
    }
    public virtual void DisplayDigit(int digit)
    {
        FormatAndDisplay(digit);
    }
    private void FormatAndDisplay(int digit)
    {
        var d = Utils.FormatDigit(digit);

        _timeTmpro.text = String.Format("{0}", d);
    }
    private void FormatAndDisplay(int hours, int minutes, int seconds)
    {
        var hour = Utils.FormatDigit(hours);
        var min = Utils.FormatDigit(minutes);
        var sec = Utils.FormatDigit(seconds);
        
        _timeTmpro.text = String.Format("{0}:{1}:{2}", hour, min, sec);
    }
    private void FormatAndDisplay(int days, int hours, int minutes, int seconds, int millisec)
    {
        var hour = Utils.FormatDigit(hours);
        var min = Utils.FormatDigit(minutes);
        var sec = Utils.FormatDigit(seconds);
        var mills = Utils.FormatDigit(millisec);
        
        if(days < 1)
            _timeTmpro.text = String.Format("{0}:{1}:{2}:{3}", hour, min, sec, mills);
        else
            _timeTmpro.text = String.Format("d{0}:{1}:{2}:{3}:{4}", days, hour, min, sec, mills);
    }
}
