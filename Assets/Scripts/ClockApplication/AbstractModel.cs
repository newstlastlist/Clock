using System;
using UniRx;

public abstract class AbstractModel : IDisposable
{
    protected CompositeDisposable _disposables = new CompositeDisposable();
    public void Dispose()
    {
        _disposables.Dispose();
    }
}
