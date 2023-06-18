using System;
using UniRx;
using UnityEngine;
using Zenject;

public class TimerPresenter : TimeControlPresenterBase
{
    [Inject] private TimerModel _timer;

    [SerializeField] private UserInputPanel _userInputPanel;

    private TimeSpan _timeFromUserInput;

    private void Awake()
    {
        base.AwakeCustom(_timer, OnStartBtnPressedAction, OnStopBtnPressedAction);
    }

    private void Start()
    {
        _timer.GetTimeRemainingAsObservable()
            .Subscribe(time => _view.DisplayTimeTimeSpan(time))
            .AddTo(_disposables);

        base.StartCustom(_timer);

        _timer.GetElapsedBoolAsObservable()
            .Subscribe(elapsed =>
            {
                if (elapsed)
                {
                    OnTimerElapsed();
                }
            }).AddTo(_disposables);
    }
    private void ResetTimeZonesUserInput()
    {
        _userInputPanel.secondsDragZone.ResetDigit();
        _userInputPanel.minutesDragZone.ResetDigit();
        _userInputPanel.hoursDragZone.ResetDigit();
    }

    private void OnStartBtnPressedAction()
    {
        _userInputPanel.SetTimeZonesInteractable(false);
        _userInputPanel.SetGoActive(false);

        _view.gameObject.SetActive(true);

        _timeFromUserInput = new TimeSpan(_userInputPanel.hoursDragZone.Digit.Value, _userInputPanel.minutesDragZone.Digit.Value,
            _userInputPanel.secondsDragZone.Digit.Value);

        _timer.SetTimerDuration(_timeFromUserInput);
        
    }
    private void OnTimerElapsed()
    {
        //return all to initial state
        base.OnStopBtnPressed(_timer, OnStopBtnPressedAction);
    }
    private void OnStopBtnPressedAction()
    {
        StartBtnsGameObjectsState();

        ResetTimeZonesUserInput();

        _userInputPanel.SetTimeZonesInteractable(true);
        _userInputPanel.SetGoActive(true);

        _view.gameObject.SetActive(false);
    }
    
}