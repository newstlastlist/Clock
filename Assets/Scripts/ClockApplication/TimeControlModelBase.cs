using System;
using UniRx;

public class TimeControlModelBase : AbstractModel
{
    protected ReactiveProperty<Boolean> _isCounting = new ReactiveProperty<Boolean>();

    protected bool _isPaused;
    
    public bool IsPaused => _isPaused;
    
    protected IDisposable _timeEntityDisposable;


    public virtual void Start()
    {
        _isCounting.Value = true;
    }

    public virtual void Stop()
    {
        _isPaused = false;
        _isCounting.Value = false;
        _timeEntityDisposable?.Dispose();
    }
    public virtual IObservable<Boolean> GetHasStartedBoolAsObservable()
    {
        return _isCounting;
    }
    public virtual void Pause()
    {
        _isPaused = true;
    }
    
    public virtual void Resume()
    {
        _isPaused = false;
    }
}
