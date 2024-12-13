using System;
using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Other
{
    [TestFixture]
    public class TestFacadeSubContainer
    {
        #region Public Nested Types

        public class FooKernel : Kernel { }

        public class InitTest : IInitializable
        {
            #region Variables

            public static bool WasRun;

            #endregion

            #region IInitializable

            public void Initialize()
            {
                WasRun = true;
            }

            #endregion
        }

        public class TickTest : ITickable
        {
            #region Variables

            public static bool WasRun;

            #endregion

            #region ITickable

            public void Tick()
            {
                WasRun = true;
            }

            #endregion
        }

        public class DisposeTest : IDisposable
        {
            #region Variables

            public static bool WasRun;

            #endregion

            #region IDisposable

            public void Dispose()
            {
                WasRun = true;
            }

            #endregion
        }

        #endregion

        #region Variables

        private static int NumInstalls;

        #endregion

        #region Public methods

        public void InstallFoo(DiContainer subContainer)
        {
            NumInstalls++;

            subContainer.Bind<FooKernel>().AsSingle();

            subContainer.Bind<IInitializable>().To<InitTest>().AsSingle();
            subContainer.Bind<ITickable>().To<TickTest>().AsSingle();
            subContainer.Bind<IDisposable>().To<DisposeTest>().AsSingle();
        }

        [Test]
        public void Test1()
        {
            NumInstalls = 0;
            InitTest.WasRun = false;
            TickTest.WasRun = false;
            DisposeTest.WasRun = false;

            DiContainer container = new DiContainer();

            container.Bind(typeof(TickableManager), typeof(InitializableManager), typeof(DisposableManager))
                .ToSelf().AsSingle().CopyIntoAllSubContainers();

            // This is how you add ITickables / etc. within sub containers
            container.BindInterfacesAndSelfTo<FooKernel>()
                .FromSubContainerResolve().ByMethod(InstallFoo).AsSingle();

            TickableManager tickManager = container.Resolve<TickableManager>();
            InitializableManager initManager = container.Resolve<InitializableManager>();
            DisposableManager disposeManager = container.Resolve<DisposableManager>();

            Assert.That(!InitTest.WasRun);
            Assert.That(!TickTest.WasRun);
            Assert.That(!DisposeTest.WasRun);

            initManager.Initialize();
            tickManager.Update();
            disposeManager.Dispose();

            Assert.That(InitTest.WasRun);
            Assert.That(TickTest.WasRun);
            Assert.That(DisposeTest.WasRun);
        }

        #endregion
    }
}