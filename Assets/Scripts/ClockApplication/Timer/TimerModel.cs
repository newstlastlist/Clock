using System;
using UniRx;
using Zenject;

public class TimerModel : AbstractModel
{
    private readonly ReactiveProperty<TimeSpan> _timeRemaining = new ReactiveProperty<TimeSpan>();
    private ReactiveProperty<Boolean> _isCounting = new ReactiveProperty<Boolean>();
    private bool _isPaused;

    private IDisposable _timerDisposable;
    private AudioManager _audioManager;

    public bool IsPaused => _isPaused;

    [Inject]
    public void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    public void StartTimer(TimeSpan duration)
    {
        _isCounting.Value = true;
        
        if (_isPaused)
        {
            _isPaused = false;
            return;
        }

        _timeRemaining.Value = duration;
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
    private void OnTimerElapsed()
    {
        _audioManager.Play(AudioManager.Sounds.TimerFinish);
        _timeRemaining.Value = TimeSpan.Zero;
        _isCounting.Value = false;
    }
    public IObservable<TimeSpan> GetTimeRemainingAsObservable()
    {
        return _timeRemaining;
    }

    public IObservable<Boolean> GetHasStartedBoolAsObservable()
    {
        return _isCounting;
    }
    public void Pause()
    {
        _isPaused = true;
    }
    public void StopTimer()
    {
        _isPaused = false;
        _timeRemaining.Value = TimeSpan.Zero;
        _timerDisposable?.Dispose();
    }
    public void ResetTimer()
    {
    }
}