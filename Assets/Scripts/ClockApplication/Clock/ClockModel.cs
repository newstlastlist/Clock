using System;
using UniRx;

public class ClockModel : AbstractModel
{
    private ReactiveProperty<DateTime> _currentTime = new ReactiveProperty<DateTime>(DateTime.Now);
    private IScheduler _scheduler;
    public ClockModel(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }
    public void Start()
    {
        _currentTime.Value = DateTime.Now;
        
        Observable.Interval(TimeSpan.FromSeconds(1), _scheduler)
            .Subscribe(_ => _currentTime.Value = DateTime.Now)
            .AddTo(_disposables);
    }
    public IObservable<DateTime> GetCurrentTimeAsObservable()
    {
        return _currentTime;
    }
}
