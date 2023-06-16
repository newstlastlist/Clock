
using UniRx;
using UnityEngine;

public class DigitPresenter : AbstractPresenter
{
    [SerializeField] private DigitView _digitView;
    [SerializeField] private ChangeTimeDrag _dragZone;

    private void Start()
    {
        _dragZone.GetDigitAsObservable()
            .Subscribe(digit => _digitView.DisplayDigit(digit))
            .AddTo(_disposables);
    }

    
}
