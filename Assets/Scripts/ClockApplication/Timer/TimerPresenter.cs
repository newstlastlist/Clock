using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerPresenter : AbstractPresenter
{
    [Inject] private TimerModel _timer;
    [SerializeField] private TimerView _timerView;

    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _startResumeBtn;
    [SerializeField] private Button _resetBtn;
    [SerializeField] private Button _stopBtn;

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
    }

    private void InitBtns()
    {
        StartBtnsGameObjectsState();

        _pauseBtn.onClick.AddListener(() => OnPauseBtnPressed());
        _startResumeBtn.onClick.AddListener(() => OnResumeBtnPressed());
        _resetBtn.onClick.AddListener(() => _timer.ResetTimer());
        _stopBtn.onClick.AddListener(() => OnStopBtnPressed());
    }

    private void StartBtnsGameObjectsState()
    {
        _pauseBtn.gameObject.SetActive(false);
        _stopBtn.gameObject.SetActive(false);
        
        _resetBtn.gameObject.SetActive(true);
        _startResumeBtn.gameObject.SetActive(true);
    }
    private void OnPauseBtnPressed()
    {
        _timer.Pause();
        _pauseBtn.gameObject.SetActive(false);
        _startResumeBtn.gameObject.SetActive(true);
    }

    private void OnResumeBtnPressed()
    {
        //when timer is paused play btn must fulfill the role of resume btn
        if (_timer.IsPaused)
        {
            _timer.StartTimer(new TimeSpan(0, 0, 0, 15));
            _pauseBtn.gameObject.SetActive(true);
            _startResumeBtn.gameObject.SetActive(false);
        }
        //when timer is NOT paused play btn must fulfill the role of play btn
        else
        {
            _timer.StartTimer(new TimeSpan(0, 0, 0, 15));
        }
    }

    private void OnStopBtnPressed()
    {
        _timer.StopTimer();

        StartBtnsGameObjectsState();

    }
    private void OnTimerStarted(bool isCounting)
    {
        if (isCounting)
        {
            _resetBtn.gameObject.SetActive(false);
            _startResumeBtn.gameObject.SetActive(false);

            _pauseBtn.gameObject.SetActive(true);
            _stopBtn.gameObject.SetActive(true);
        }
        else
        {
            _resetBtn.gameObject.SetActive(true);
            _startResumeBtn.gameObject.SetActive(true);

            _pauseBtn.gameObject.SetActive(false);
            _stopBtn.gameObject.SetActive(false);
        }
    }
}