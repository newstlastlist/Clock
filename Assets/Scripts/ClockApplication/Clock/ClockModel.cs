using System;
using UniRx;
using UnityEngine;

public class ClockModel : AbstractModel
{
    private ReactiveProperty<DateTime> _currentTime = new ReactiveProperty<DateTime>(DateTime.Now);
    private void Start()
    {
        _currentTime.Value = DateTime.Now;
        
        Observable.Interval(TimeSpan.FromSeconds(1), Scheduler.MainThreadIgnoreTimeScale)
            .Subscribe(_ => _currentTime.Value = DateTime.Now)
            .AddTo(_disposables);
    }
    public IObservable<DateTime> GetCurrentTimeAsObservable()
    {
        return _currentTime;
    }

   
}
