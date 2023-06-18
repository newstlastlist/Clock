using Zenject;

public class StopWatchInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<StopWatchModel>()
            .AsSingle()
            .NonLazy();
    }
}
