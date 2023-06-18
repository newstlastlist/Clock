using System;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimeControlPresenterBase : AbstractPresenter
{
    [SerializeField] protected AbstractClocklView _view;
    
    [SerializeField] protected Button _pauseBtn;
    [SerializeField] protected Button _startBtn;
    [SerializeField] protected Button _resumeBtn;
    [SerializeField] protected Button _stopBtn;

    protected void StartCustom(TimeControlModelBase model)
    {
        model.GetHasStartedBoolAsObservable()
            .Subscribe(isCounting => OnTimerStarted(isCounting))
            .AddTo(_disposables);
    }
    protected void AwakeCustom(TimeControlModelBase timeControlModel, Action onStartPressed, Action onStopPressed)
    {
        InitBtns(timeControlModel, onStartPressed, onStopPressed);
    }
    protected virtual void InitBtns(TimeControlModelBase timeControlModel, Action onStartPressed, Action onStopPressed)
    {
        StartBtnsGameObjectsState();

        _pauseBtn.onClick.AddListener(() => OnPauseBtnPressed(timeControlModel));
        _startBtn.onClick.AddListener(() => OnStartBtnPressed(timeControlModel, onStartPressed));
        _resumeBtn.onClick.AddListener(() => OnResumeBtnPressed(timeControlModel));
        _stopBtn.onClick.AddListener(() => OnStopBtnPressed(timeControlModel, onStopPressed));
    }
    protected virtual void OnResumeBtnPressed(TimeControlModelBase timeControlModel)
    {
        timeControlModel.Resume();

        _pauseBtn.gameObject.SetActive(true);

        _resumeBtn.gameObject.SetActive(false);
    }
    protected virtual void OnPauseBtnPressed(TimeControlModelBase timeControlModel)
    {
        _pauseBtn.gameObject.SetActive(false);
        _startBtn.gameObject.SetActive(false);

        _resumeBtn.gameObject.SetActive(true);
        
        timeControlModel.Pause();
    }

    protected virtual void OnStopBtnPressed(TimeControlModelBase timeControlModel, Action onStopPressed)
    {
        onStopPressed?.Invoke();
        timeControlModel.Stop();
    }
    protected virtual void OnStartBtnPressed(TimeControlModelBase timeControlModel, Action onStartPressed)
    {
        onStartPressed?.Invoke();
        timeControlModel.Start();
    }
    protected virtual void StartBtnsGameObjectsState()
    {
        _pauseBtn.gameObject.SetActive(false);
        _stopBtn.gameObject.SetActive(false);
        _resumeBtn.gameObject.SetActive(false);

        _startBtn.gameObject.SetActive(true);
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

            _pauseBtn.gameObject.SetActive(false);
            _stopBtn.gameObject.SetActive(false);
            _resumeBtn.gameObject.SetActive(false);
        }
    }
}
