using Zenject;

namespace Playground.Services.Input
{
    public class InputServiceInstaller : Installer<InputServiceInstaller>
    {
        #region Public methods

        public override void InstallBindings()
        {
            Container.Bind<InputService>().FromNewComponentOnNewGameObject().AsSingle();
        }

        #endregion
    }
}