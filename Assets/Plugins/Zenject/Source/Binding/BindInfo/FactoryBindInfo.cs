using System;
using System.Collections.Generic;

namespace Zenject
{
    [NoReflectionBaking]
    public class FactoryBindInfo
    {
        #region Properties

        public List<TypeValuePair> Arguments { get; set; }

        public Type FactoryType { get; private set; }

        public Func<DiContainer, IProvider> ProviderFunc { get; set; }

        #endregion

        #region Setup/Teardown

        public FactoryBindInfo(Type factoryType)
        {
            FactoryType = factoryType;
            Arguments = new List<TypeValuePair>();
        }

        #endregion
    }
}