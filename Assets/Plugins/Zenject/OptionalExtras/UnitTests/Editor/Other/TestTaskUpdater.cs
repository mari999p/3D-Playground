using System;
using ModestTree.Util;
using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Other
{
    [TestFixture]
    public class TestTaskUpdater
    {
        #region Variables

        private DiContainer _container;

        #endregion

        #region Public methods

        public void BindTickable<TTickable>(int priority) where TTickable : ITickable
        {
            _container.BindInterfacesAndSelfTo<TTickable>().AsSingle();
            _container.Bind<ValuePair<Type, int>>().FromInstance(ValuePair.New(typeof(TTickable), priority));
        }

        [SetUp]
        public void Setup()
        {
            _container = new DiContainer();

            _container.Bind<TaskUpdater<ITickable>>().FromInstance(new TickablesTaskUpdater());
        }

        [Test]
        // Test that tickables get called in the correct order
        public void TestOrder()
        {
            BindTickable<Tickable3>(2);
            BindTickable<Tickable1>(0);
            BindTickable<Tickable2>(1);

            TaskUpdater<ITickable> taskUpdater = _container.Resolve<TaskUpdater<ITickable>>();

            Tickable1 tick1 = _container.Resolve<Tickable1>();
            Tickable2 tick2 = _container.Resolve<Tickable2>();
            Tickable3 tick3 = _container.Resolve<Tickable3>();

            int tickCount = 0;

            tick1.TickCalled += delegate
            {
                Assert.IsEqual(tickCount, 0);
                tickCount++;
            };

            tick2.TickCalled += delegate
            {
                Assert.IsEqual(tickCount, 1);
                tickCount++;
            };

            tick3.TickCalled += delegate
            {
                Assert.IsEqual(tickCount, 2);
                tickCount++;
            };

            taskUpdater.UpdateAll();
        }

        [Test]
        public void TestTickablesAreOptional()
        {
            Assert.IsNotNull(_container.Resolve<TaskUpdater<ITickable>>());
        }

        #endregion

        #region Local data

        private class Tickable1 : ITickable
        {
            #region Events

            public event Action TickCalled = delegate { };

            #endregion

            #region ITickable

            public void Tick()
            {
                TickCalled();
            }

            #endregion
        }

        private class Tickable2 : ITickable
        {
            #region Events

            public event Action TickCalled = delegate { };

            #endregion

            #region ITickable

            public void Tick()
            {
                TickCalled();
            }

            #endregion
        }

        private class Tickable3 : ITickable
        {
            #region Events

            public event Action TickCalled = delegate { };

            #endregion

            #region ITickable

            public void Tick()
            {
                TickCalled();
            }

            #endregion
        }

        #endregion
    }
}