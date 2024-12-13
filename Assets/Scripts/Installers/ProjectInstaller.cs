using Zenject;

namespace DefaultNamespace;

public class ProjectInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        InputServiceInstaller.Install(Container);
    }
}
}
{
    
}