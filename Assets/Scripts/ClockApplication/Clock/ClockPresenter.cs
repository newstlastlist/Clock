using UniRx;
using UnityEngine;

public class ClockPresenter : AbstractPresenter
{
    [SerializeField] private AbstractClocklView _clockView;
    
    [SerializeField] private ClockModel _clock;

    private void Start()
    {
        _clock.GetCurrentTimeAsObservable()
            .Subscribe(time => _clockView.DisplayTimeDateTime(time))
            .AddTo(_disposables);
    }
}