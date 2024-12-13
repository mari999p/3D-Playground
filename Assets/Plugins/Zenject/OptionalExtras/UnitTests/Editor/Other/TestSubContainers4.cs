using System.Collections.Generic;
using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Other
{
    [TestFixture]
    public class TestSubContainers4 : ZenjectUnitTestFixture
    {
        #region Public Nested Types

        public class RotorController
        {
            #region Variables

            [Inject]
            public RotorModel Model;

            #endregion
        }

        public class RotorView
        {
            #region Variables

            [Inject]
            public RotorController Controller;

            [Inject]
            public RotorModel Model;

            #endregion
        }

        public class RotorModel { }

        #endregion

        #region Variables

        private readonly Dictionary<object, DiContainer> _subContainers = new();

        #endregion

        #region Public methods

        [Test]
        public void RunTest()
        {
            SetupContainer();

            RotorView view1 = Container.Resolve<RotorView>();

            Assert.IsEqual(view1.Controller.Model, view1.Model);

            RotorView view2 = Container.Resolve<RotorView>();

            Assert.IsEqual(view2.Controller.Model, view2.Model);

            Assert.IsNotEqual(view2.Model, view1.Model);
            Assert.IsNotEqual(view2, view1);
        }

        #endregion

        #region Private methods

        private void InstallViewBindings(DiContainer subContainer)
        {
            subContainer.Bind<RotorController>().AsSingle();
            subContainer.Bind<RotorModel>().AsSingle();
        }

        private void SetupContainer()
        {
            Container.Bind<RotorController>().FromMethod(SubContainerResolve<RotorController>).AsTransient()
                .WhenInjectedInto<RotorView>();

            Container.Bind<RotorModel>().FromMethod(SubContainerResolve<RotorModel>).AsTransient()
                .WhenInjectedInto<RotorView>();

            Container.Bind<RotorView>().AsTransient();
        }

        private T SubContainerResolve<T>(InjectContext context)
        {
            Assert.IsNotNull(context.ObjectInstance);
            DiContainer subContainer;

            if (!_subContainers.TryGetValue(context.ObjectInstance, out subContainer))
            {
                subContainer = context.Container.CreateSubContainer();
                _subContainers.Add(context.ObjectInstance, subContainer);

                InstallViewBindings(subContainer);
            }

            return (T)subContainer.Resolve(context);
        }

        #endregion
    }
}