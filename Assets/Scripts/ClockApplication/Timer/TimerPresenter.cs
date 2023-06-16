using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerPresenter : AbstractPresenter
{
    [Inject] private TimerModel _timer;
    [SerializeField] private TimerView _timerView;

    [SerializeField] private UserInputPanel _userInputPanel;

    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _stopBtn;


    private TimeSpan _timeFromUserInput;

    private void Awake()
    {
        InitBtns();
    }

    private void Start()
    {
        _timer.GetTimeRemainingAsObservable()
            .Subscribe(time => _timerView.DisplayTimeTimeSpan(time))
            .AddTo(_disposables);

        _timer.GetHasStartedBoolAsObservable()
            .Subscribe(isCounting => OnTimerStarted(isCounting))
            .AddTo(_disposables);

        _timer.GetElapsedBoolAsObservable()
            .Subscribe(elapsed =>
            {
                if (elapsed)
                {
                    OnTimerElapsed();
                }
            }).AddTo(_disposables);
    }

    private void InitBtns()
    {
        StartBtnsGameObjectsState();

        _pauseBtn.onClick.AddListener(() => OnPauseBtnPressed());
        _startBtn.onClick.AddListener(() => OnStartBtnPressed());
        _resumeBtn.onClick.AddListener(() => OnResumeBtnPressed());
        _stopBtn.onClick.AddListener(() => OnStopBtnPressed());
    }

    private void ResetTimeZonesUserInput()
    {
        _userInputPanel.secondsDragZone.ResetDigit();
        _userInputPanel.minutesDragZone.ResetDigit();
        _userInputPanel.hoursDragZone.ResetDigit();
    }

    private void StartBtnsGameObjectsState()
    {
        _pauseBtn.gameObject.SetActive(false);
        _stopBtn.gameObject.SetActive(false);
        _resumeBtn.gameObject.SetActive(false);

        _startBtn.gameObject.SetActive(true);
    }

    private void OnPauseBtnPressed()
    {
        _timer.Pause();

        _pauseBtn.gameObject.SetActive(false);
        _startBtn.gameObject.SetActive(false);

        _resumeBtn.gameObject.SetActive(true);
    }

    private void OnStartBtnPressed()
    {
        _userInputPanel.SetTimeZonesInteractable(false);
        _userInputPanel.SetGoActive(false);

        _timerView.gameObject.SetActive(true);

        _timeFromUserInput = new TimeSpan(_userInputPanel.hoursDragZone.Digit.Value, _userInputPanel.minutesDragZone.Digit.Value,
            _userInputPanel.secondsDragZone.Digit.Value);

        _timer.SetTimerDuration(_timeFromUserInput);
        _timer.StartTimer();
    }

    private void OnResumeBtnPressed()
    {
        _timer.Resume();

        _pauseBtn.gameObject.SetActive(true);
        
        _resumeBtn.gameObject.SetActive(false);
    }

    private void OnTimerElapsed()
    {
        //return all to initial state
        OnStopBtnPressed();
    }
    private void OnStopBtnPressed()
    {
        _timer.StopTimer();

        StartBtnsGameObjectsState();

        ResetTimeZonesUserInput();

        _userInputPanel.SetTimeZonesInteractable(true);
        _userInputPanel.SetGoActive(true);

        _timerView.gameObject.SetActive(false);
    }

    private void OnTimerStarted(bool isCounting)
    {
        if (isCounting)
        {
            _startBtn.gameObject.SetActive(false);
            _resumeBtn.gameObject.SetActive(false);

            _pauseBtn.gameObject.SetActive(true);
            _stopBtn.gameObject.SetActive(true);
        }
        else
        {
            _startBtn.gameObject.SetActive(true);
            ;

            _pauseBtn.gameObject.SetActive(false);
            _stopBtn.gameObject.SetActive(false);
            _resumeBtn.gameObject.SetActive(false);
        }
    }
}