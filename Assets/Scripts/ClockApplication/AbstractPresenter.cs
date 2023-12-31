using System;
using UniRx;
using UnityEngine;

public class AbstractPresenter : MonoBehaviour, IDisposable
{
    protected CompositeDisposable _disposables = new CompositeDisposable();

    private void OnDestroy()
    {
        Dispose();
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
