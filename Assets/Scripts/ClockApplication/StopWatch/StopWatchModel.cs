using System;
using UniRx;

public class StopWatchModel : TimeControlModelBase
{
    private ReactiveProperty<TimeSpan> _elapsedTime = new ReactiveProperty<TimeSpan>(TimeSpan.Zero);

    public override void Start()
    {
        base.Start();
        
        _timeEntityDisposable = Observable.Interval(TimeSpan.FromMilliseconds(20))
            .Subscribe(_ =>
            {
                if (!_isPaused)
                    _elapsedTime.Value += TimeSpan.FromMilliseconds(20);
            });
    }

    public IObservable<TimeSpan> GetElapsedTimeAsObservable()
    {
        return _elapsedTime;
    }
    public TimeSpan GetCurrentElapsedTime()
    {
        return _elapsedTime.Value;
    }
    public override void Stop()
    {
        base.Stop();
        _elapsedTime.Value = TimeSpan.Zero;
    }
        
}
