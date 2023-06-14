using UniRx;
using UnityEngine;
using Zenject;

public class ClockPresenter : MonoBehaviour
{
    [SerializeField] private AbstractCloaklView _clockView;
    
    private ClockModel _clock;
    
    private CompositeDisposable _disposables = new CompositeDisposable();

    public AbstractCloaklView CloackView
    {
        get => _clockView;
        set => _clockView = value;
    }

    [Inject]
    public void Construct(ClockModel clock)
    {
        _clock = clock;
    }

    private void Start()
    {
        _clock.GetCurrentTimeAsObservable()
            .Subscribe(time => _clockView.DisplayTime(time))
            .AddTo(_disposables);
    }
}