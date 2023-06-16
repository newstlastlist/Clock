using System;
using UniRx;
using UnityEngine;
using Zenject;

public class TimerModel : AbstractModel
{
    private readonly ReactiveProperty<TimeSpan> _timeRemaining = new ReactiveProperty<TimeSpan>();
    private ReactiveProperty<Boolean> _isCounting = new ReactiveProperty<Boolean>();
    private ReactiveProperty<Boolean> _elapsed = new ReactiveProperty<Boolean>();
    private bool _isPaused;

    private IDisposable _timerDisposable;
    private AudioManager _audioManager;

    public bool IsPaused => _isPaused;

    [Inject]
    public void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    public void StartTimer()
    {
        _isCounting.Value = true;
        _elapsed.Value = false;
        
        _timerDisposable = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .TakeWhile(_ => _timeRemaining.Value > TimeSpan.Zero)
            .Subscribe(_ =>
                {
                    if(!_isPaused)
                        _timeRemaining.Value -= TimeSpan.FromSeconds(1);
                    // Debug.Log(_timeRemaining.Value);
                },
                () =>
                {
                    OnTimerElapsed();
                }
            );
    }

    public void SetTimerDuration(TimeSpan duration)
    {
        _timeRemaining.Value  = duration;
    }
    private void OnTimerElapsed()
    {
        _audioManager.Play(AudioManager.Sounds.TimerFinish);

        _elapsed.Value = true;
        
        StopTimer();
    }
    public IObservable<TimeSpan> GetTimeRemainingAsObservable()
    {
        return _timeRemaining;
    }

    public IObservable<Boolean> GetHasStartedBoolAsObservable()
    {
        return _isCounting;
    }
    public IObservable<Boolean> GetElapsedBoolAsObservable()
    {
        return _elapsed;
    }
    public void Pause()
    {
        _isPaused = true;
    }
    public void Resume()
    {
        _isPaused = false;
    }
    public void StopTimer()
    {
        _isPaused = false;
        _isCounting.Value = false;
        _timeRemaining.Value = TimeSpan.Zero;
        _timerDisposable?.Dispose();
    }

}