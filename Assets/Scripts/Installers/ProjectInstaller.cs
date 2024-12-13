using Playground.Services.Input;
using Zenject;

namespace Playground.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        #region Public methods

        public override void InstallBindings()
        {
            InputServiceInstaller.Install(Container);
        }

        #endregion
    }
}