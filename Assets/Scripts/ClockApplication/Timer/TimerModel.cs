using System;
using UniRx;
using Zenject;

public class TimerModel : TimeControlModelBase
{
    private readonly ReactiveProperty<TimeSpan> _timeRemaining = new ReactiveProperty<TimeSpan>();
    private ReactiveProperty<Boolean> _elapsed = new ReactiveProperty<Boolean>();

    private AudioManager _audioManager;

    [Inject]
    public void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    public override void Start()
    {
        base.Start();
        
        _elapsed.Value = false;

        _timeEntityDisposable = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .TakeWhile(_ => _timeRemaining.Value > TimeSpan.Zero)
            .Subscribe(_ =>
                {
                    if (!_isPaused)
                        _timeRemaining.Value -= TimeSpan.FromSeconds(1);
                    // Debug.Log(_timeRemaining.Value);
                },
                () => { OnTimerElapsed(); }
            );
    }

    public void SetTimerDuration(TimeSpan duration)
    {
        _timeRemaining.Value = duration;
    }

    private void OnTimerElapsed()
    {
        _audioManager.Play(AudioManager.Sounds.TimerFinish);

        _elapsed.Value = true;

        Stop();
    }

    public IObservable<TimeSpan> GetTimeRemainingAsObservable()
    {
        return _timeRemaining;
    }

    public IObservable<Boolean> GetElapsedBoolAsObservable()
    {
        return _elapsed;
    }


    public override void Stop()
    {
        base.Stop();
        _timeRemaining.Value = TimeSpan.Zero;
    }
}