using UniRx;
using Zenject;

public class ClockModelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ClockModel>()
            .AsSingle()
            .WithArguments(Scheduler.MainThreadIgnoreTimeScale)
            .NonLazy();
    }
}
