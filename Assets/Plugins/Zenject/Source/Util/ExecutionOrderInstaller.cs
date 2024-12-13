using System;
using System.Collections.Generic;

namespace Zenject
{
    public class ExecutionOrderInstaller : Installer<List<Type>, ExecutionOrderInstaller>
    {
        #region Variables

        private readonly List<Type> _typeOrder;

        #endregion

        #region Setup/Teardown

        public ExecutionOrderInstaller(List<Type> typeOrder)
        {
            _typeOrder = typeOrder;
        }

        #endregion

        #region Public methods

        public override void InstallBindings()
        {
            // All tickables without explicit priorities assigned are given order of zero,
            // so put all of these before that (ie. negative)
            int order = -1 * _typeOrder.Count;

            foreach (Type type in _typeOrder)
            {
                Container.BindExecutionOrder(type, order);
                order++;
            }
        }

        #endregion
    }
}