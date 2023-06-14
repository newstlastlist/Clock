using System;
using TMPro;
using UnityEngine;

public class ClockView : AbstractCloaklView
{
    [SerializeField] private TextMeshProUGUI _timeTmpro;
    
    public override void Display(DateTime time)
    {
        var hour = FormatDigit(time.Hour);
        var min = FormatDigit(time.Minute);
        var sec = FormatDigit(time.Second);
        
        _timeTmpro.text = String.Format("{0}:{1}:{2}", hour, min, sec);
    }

    private string FormatDigit(int digit)
    {
        if (digit < 10)
        {
            return "0" + digit.ToString();
        }
        else
        {
            return digit.ToString();
        }
    }
}
