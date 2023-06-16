using UniRx;
using UnityEngine;
using Zenject;

public class ClockPresenter : AbstractPresenter
{
    [Inject] private ClockModel _clock;
    [SerializeField] private AbstractClocklView _clockView;

    private void Start()
    {
        _clock.GetCurrentTimeAsObservable()
            .Subscribe(time => _clockView.DisplayTimeDateTime(time))
            .AddTo(_disposables);
    }
}