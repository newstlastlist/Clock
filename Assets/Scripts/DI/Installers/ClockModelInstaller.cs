using Zenject;
using UnityEngine;

public class ClockModelInstaller : MonoInstaller
{
    [SerializeField] private ClockModel _clock;
    public override void InstallBindings()
    {
        Container.Bind<ClockModel>()
            .FromInstance(_clock)
            .AsSingle()
            .NonLazy();
    }
}
