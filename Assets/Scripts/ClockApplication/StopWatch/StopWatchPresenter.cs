using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StopWatchPresenter : TimeControlPresenterBase
{
    [Inject] private StopWatchModel _stopWatch;
    
    [SerializeField] private LapTimeHistory _lapTimeHistory;

    [SerializeField] private Button _lapTimeBtn;
    private void Awake()
    {
        base.AwakeCustom(_stopWatch, null, OnStopPressedAction);
        
        _lapTimeBtn.onClick.AddListener(() => _lapTimeHistory.LapTime(_stopWatch.GetCurrentElapsedTime()));
    }

    private void Start()
    {
        _stopWatch.GetElapsedTimeAsObservable()
            .Subscribe(time => _view.DisplayTimeTimeSpanWithMillisecondsAndDays(time))
            .AddTo(_disposables);

        _stopWatch.GetHasStartedBoolAsObservable()
            .Subscribe(hasStarted =>
            {
                _lapTimeBtn.interactable = hasStarted;
            }).AddTo(_disposables);
        
        base.StartCustom(_stopWatch);
    }

    private void OnStopPressedAction()
    {
        _lapTimeHistory.ClearHistory();
    }
}
