using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
    [NoReflectionBaking]
    public class SubContainerBindingFinalizer : ProviderBindingFinalizer
    {
        #region Variables

        private readonly Func<DiContainer, ISubContainerCreator> _creatorFactory;
        private readonly bool _resolveAll;
        private readonly object _subIdentifier;

        #endregion

        #region Setup/Teardown

        public SubContainerBindingFinalizer(
            BindInfo bindInfo, object subIdentifier,
            bool resolveAll, Func<DiContainer, ISubContainerCreator> creatorFactory)
            : base(bindInfo)
        {
            _subIdentifier = subIdentifier;
            _resolveAll = resolveAll;
            _creatorFactory = creatorFactory;
        }

        #endregion

        #region Protected methods

        protected override void OnFinalizeBinding(DiContainer container)
        {
            if (BindInfo.ToChoice == ToChoices.Self)
            {
                Assert.IsEmpty(BindInfo.ToTypes);
                FinalizeBindingSelf(container);
            }
            else
            {
                FinalizeBindingConcrete(container, BindInfo.ToTypes);
            }
        }

        #endregion

        #region Private methods

        private void FinalizeBindingConcrete(DiContainer container, List<Type> concreteTypes)
        {
            ScopeTypes scope = GetScope();

            switch (scope)
            {
                case ScopeTypes.Transient:
                {
                    RegisterProvidersForAllContractsPerConcreteType(
                        container,
                        concreteTypes,
                        (_, concreteType) =>
                            new SubContainerDependencyProvider(
                                concreteType, _subIdentifier, _creatorFactory(container), _resolveAll));
                    break;
                }
                case ScopeTypes.Singleton:
                {
                    SubContainerCreatorCached containerCreator =
                        new SubContainerCreatorCached(_creatorFactory(container));

                    RegisterProvidersForAllContractsPerConcreteType(
                        container,
                        concreteTypes,
                        (_, concreteType) =>
                            new SubContainerDependencyProvider(
                                concreteType, _subIdentifier, containerCreator, _resolveAll));
                    break;
                }
                default:
                {
                    throw Assert.CreateException();
                }
            }
        }

        private void FinalizeBindingSelf(DiContainer container)
        {
            ScopeTypes scope = GetScope();

            switch (scope)
            {
                case ScopeTypes.Transient:
                {
                    RegisterProviderPerContract(
                        container,
                        (_, contractType) => new SubContainerDependencyProvider(
                            contractType, _subIdentifier, _creatorFactory(container), _resolveAll));
                    break;
                }
                case ScopeTypes.Singleton:
                {
                    SubContainerCreatorCached containerCreator =
                        new SubContainerCreatorCached(_creatorFactory(container));

                    RegisterProviderPerContract(
                        container,
                        (_, contractType) =>
                            new SubContainerDependencyProvider(
                                contractType, _subIdentifier, containerCreator, _resolveAll));
                    break;
                }
                default:
                {
                    throw Assert.CreateException();
                }
            }
        }

        #endregion
    }
}