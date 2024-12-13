using System;
using System.Collections.Generic;
using ModestTree;
using Zenject.Internal;

namespace Zenject
{
    [NoReflectionBaking]
    public class BindStatement : IDisposable
    {
        #region Variables

        private readonly List<IDisposable> _disposables;
        private IBindingFinalizer _bindingFinalizer;

        #endregion

        #region Properties

        public BindingInheritanceMethods BindingInheritanceMethod
        {
            get
            {
                AssertHasFinalizer();
                return _bindingFinalizer.BindingInheritanceMethod;
            }
        }

        public bool HasFinalizer => _bindingFinalizer != null;

        #endregion

        #region Setup/Teardown

        public BindStatement()
        {
            _disposables = new List<IDisposable>();
            Reset();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            ZenPools.DespawnStatement(this);
        }

        #endregion

        #region Public methods

        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void FinalizeBinding(DiContainer container)
        {
            AssertHasFinalizer();
            _bindingFinalizer.FinalizeBinding(container);
        }

        public void Reset()
        {
            _bindingFinalizer = null;

            for (int i = 0; i < _disposables.Count; i++)
            {
                _disposables[i].Dispose();
            }

            _disposables.Clear();
        }

        public void SetFinalizer(IBindingFinalizer bindingFinalizer)
        {
            _bindingFinalizer = bindingFinalizer;
        }

        public BindInfo SpawnBindInfo()
        {
            BindInfo bindInfo = ZenPools.SpawnBindInfo();
            AddDisposable(bindInfo);
            return bindInfo;
        }

        #endregion

        #region Private methods

        private void AssertHasFinalizer()
        {
            if (_bindingFinalizer == null)
            {
                throw Assert.CreateException(
                    "Unfinished binding!  Some required information was left unspecified.");
            }
        }

        #endregion
    }
}