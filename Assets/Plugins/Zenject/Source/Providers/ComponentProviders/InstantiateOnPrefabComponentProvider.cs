#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
    [NoReflectionBaking]
    public class InstantiateOnPrefabComponentProvider : IProvider
    {
        #region Variables

        private readonly Type _componentType;
        private readonly IPrefabInstantiator _prefabInstantiator;

        #endregion

        #region Properties

        public bool IsCached => false;

        public bool TypeVariesBasedOnMemberType => false;

        #endregion

        #region Setup/Teardown

        // if concreteType is null we use the contract type from inject context
        public InstantiateOnPrefabComponentProvider(
            Type componentType,
            IPrefabInstantiator prefabInstantiator)
        {
            _prefabInstantiator = prefabInstantiator;
            _componentType = componentType;
        }

        #endregion

        #region IProvider

        public Type GetInstanceType(InjectContext context)
        {
            return _componentType;
        }

        public void GetAllInstancesWithInjectSplit(
            InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
        {
            Assert.IsNotNull(context);

            GameObject gameObject = _prefabInstantiator.Instantiate(context, args, out injectAction);

            Component component = gameObject.AddComponent(_componentType);

            buffer.Add(component);
        }

        #endregion
    }
}

#endif