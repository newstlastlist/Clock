using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClockApplication : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private ClockView _clockView;

    public void ShowTimer(Button btn)
    {
        HidePressedBtnEnableAllOtherBtns(btn);
        _clockView.MoveToBottomPos();
    }

    private void HidePressedBtnEnableAllOtherBtns(Button btn)
    {
        btn.gameObject.SetActive(false);
        
        _buttons.ForEach(b =>
        {
            if(b != btn)
                b.gameObject.SetActive(true);
        });
    }
}
