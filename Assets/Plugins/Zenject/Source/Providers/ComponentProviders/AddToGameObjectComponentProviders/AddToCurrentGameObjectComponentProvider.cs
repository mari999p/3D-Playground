#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject
{
    [NoReflectionBaking]
    public class AddToCurrentGameObjectComponentProvider : IProvider
    {
        #region Variables

        private readonly Type _componentType;
        private readonly object _concreteIdentifier;
        private readonly DiContainer _container;
        private readonly List<TypeValuePair> _extraArguments;
        private readonly Action<InjectContext, object> _instantiateCallback;

        #endregion

        #region Properties

        public bool IsCached => false;

        public bool TypeVariesBasedOnMemberType => false;

        protected Type ComponentType => _componentType;

        protected DiContainer Container => _container;

        #endregion

        #region Setup/Teardown

        public AddToCurrentGameObjectComponentProvider(
            DiContainer container, Type componentType,
            IEnumerable<TypeValuePair> extraArguments, object concreteIdentifier,
            Action<InjectContext, object> instantiateCallback)
        {
            Assert.That(componentType.DerivesFrom<Component>());

            _extraArguments = extraArguments.ToList();
            _componentType = componentType;
            _container = container;
            _concreteIdentifier = concreteIdentifier;
            _instantiateCallback = instantiateCallback;
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

            Assert.That(context.ObjectType.DerivesFrom<Component>(),
                "Object '{0}' can only be injected into MonoBehaviour's since it was bound with 'FromNewComponentSibling'. Attempted to inject into non-MonoBehaviour '{1}'",
                context.MemberType, context.ObjectType);

            object instance;

            if (!_container.IsValidating || TypeAnalyzer.ShouldAllowDuringValidation(_componentType))
            {
                GameObject gameObj = ((Component)context.ObjectInstance).gameObject;

                Component componentInstance = gameObj.GetComponent(_componentType);
                instance = componentInstance;

                // Use componentInstance so that it triggers unity's overloaded comparison operator
                // So if the component is there but missing then it returns null
                // (https://github.com/svermeulen/Zenject/issues/582)
                if (componentInstance != null)
                {
                    injectAction = null;
                    buffer.Add(instance);
                    return;
                }

                instance = gameObj.AddComponent(_componentType);
            }
            else
            {
                instance = new ValidationMarker(_componentType);
            }

            // Note that we don't just use InstantiateComponentOnNewGameObjectExplicit here
            // because then circular references don't work

            injectAction = () =>
            {
                List<TypeValuePair> extraArgs = ZenPools.SpawnList<TypeValuePair>();

                extraArgs.AllocFreeAddRange(_extraArguments);
                extraArgs.AllocFreeAddRange(args);

                _container.InjectExplicit(instance, _componentType, extraArgs, context, _concreteIdentifier);

                Assert.That(extraArgs.IsEmpty());
                ZenPools.DespawnList(extraArgs);

                if (_instantiateCallback != null)
                {
                    _instantiateCallback(context, instance);
                }
            };

            buffer.Add(instance);
        }

        #endregion
    }
}

#endif