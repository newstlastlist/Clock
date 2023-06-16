using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class ClockApplication : MonoBehaviour
{
    [Inject] private ClockModel _clock;
    [SerializeField] private List<Button> _buttons;
    private void Start()
    {
        _clock.Start();
    }
    private void OnDestroy()
    {
        _clock.Dispose();
    }
    public void ShowTimer(Button btn)
    {
        HidePressedBtnEnableAllOtherBtns(btn);
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
