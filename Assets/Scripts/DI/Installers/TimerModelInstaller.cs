using Zenject;

public class TimerModelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TimerModel>()
            .AsSingle()
            .NonLazy();
    }
}
