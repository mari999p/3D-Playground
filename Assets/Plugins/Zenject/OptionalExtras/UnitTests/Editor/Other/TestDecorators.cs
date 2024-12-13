using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Other
{
    [TestFixture]
    public class TestDecorators : ZenjectUnitTestFixture
    {
        #region Public Nested Types

        public interface ISaveHandler
        {
            #region Public methods

            void Save();

            #endregion
        }

        public class SaveHandler : ISaveHandler
        {
            #region Properties

            public static int CallCount { get; set; }

            public static int NumInstances { get; set; }

            #endregion

            #region Setup/Teardown

            public SaveHandler()
            {
                NumInstances++;
            }

            #endregion

            #region ISaveHandler

            public void Save()
            {
                CallCount = CallCounter++;
            }

            #endregion
        }

        public class SaveDecorator1 : ISaveHandler
        {
            #region Variables

            private readonly ISaveHandler _handler;

            #endregion

            #region Properties

            public static int CallCount { get; set; }

            public static int NumInstances { get; set; }

            #endregion

            #region Setup/Teardown

            public SaveDecorator1(ISaveHandler handler)
            {
                _handler = handler;
                NumInstances++;
            }

            #endregion

            #region ISaveHandler

            public void Save()
            {
                CallCount = CallCounter++;
                _handler.Save();
            }

            #endregion
        }

        public class SaveDecorator2 : ISaveHandler
        {
            #region Variables

            private readonly ISaveHandler _handler;

            #endregion

            #region Properties

            public static int CallCount { get; set; }

            #endregion

            #region Setup/Teardown

            public SaveDecorator2(ISaveHandler handler)
            {
                _handler = handler;
            }

            #endregion

            #region ISaveHandler

            public void Save()
            {
                CallCount = CallCounter++;
                _handler.Save();
            }

            #endregion
        }

        public class Foo { }

        #endregion

        #region Variables

        private static int CallCounter;

        #endregion

        #region Public methods

        [Test]
        public void TestCaching()
        {
            Container.Bind<ISaveHandler>().To<SaveHandler>().AsTransient();
            Container.Decorate<ISaveHandler>().With<SaveDecorator1>();

            SaveHandler.NumInstances = 0;
            SaveDecorator1.NumInstances = 0;

            Container.Resolve<ISaveHandler>().Save();

            Assert.IsEqual(SaveHandler.NumInstances, 1);
            Assert.IsEqual(SaveDecorator1.NumInstances, 1);

            Container.Resolve<ISaveHandler>().Save();

            Assert.IsEqual(SaveHandler.NumInstances, 2);
            Assert.IsEqual(SaveDecorator1.NumInstances, 2);
        }

        [Test]
        public void TestCaching2()
        {
            Container.Bind<ISaveHandler>().To<SaveHandler>().AsCached();
            Container.Decorate<ISaveHandler>().With<SaveDecorator1>();

            SaveHandler.NumInstances = 0;
            SaveDecorator1.NumInstances = 0;

            Container.Resolve<ISaveHandler>().Save();

            Assert.IsEqual(SaveHandler.NumInstances, 1);
            Assert.IsEqual(SaveDecorator1.NumInstances, 1);

            Container.Resolve<ISaveHandler>().Save();

            Assert.IsEqual(SaveHandler.NumInstances, 1);
            Assert.IsEqual(SaveDecorator1.NumInstances, 1);
        }

        [Test]
        public void TestContainerInheritance()
        {
            Container.Bind<ISaveHandler>().To<SaveHandler>().AsSingle();
            Container.Decorate<ISaveHandler>().With<SaveDecorator1>();

            DiContainer subContainer = Container.CreateSubContainer();

            CallCounter = 1;
            SaveHandler.CallCount = 0;
            SaveDecorator1.CallCount = 0;

            subContainer.Resolve<ISaveHandler>().Save();

            Assert.IsEqual(SaveDecorator1.CallCount, 1);
            Assert.IsEqual(SaveHandler.CallCount, 2);
        }

        [Test]
        public void TestDecoratorMethod()
        {
            SaveHandler.NumInstances = 0;
            SaveDecorator1.CallCount = 0;

            bool wasCalled = false;

            Container.Bind<ISaveHandler>().To<SaveHandler>().AsSingle();
            Container.Decorate<ISaveHandler>()
                .With<SaveDecorator1>().FromMethod((x, h) =>
                {
                    wasCalled = true;
                    return new SaveDecorator1(h);
                });

            CallCounter = 1;
            Assert.That(!wasCalled);
            Assert.IsEqual(SaveHandler.NumInstances, 0);
            Assert.IsEqual(SaveDecorator1.CallCount, 0);

            Container.Resolve<ISaveHandler>().Save();

            Assert.That(wasCalled);
            Assert.IsEqual(SaveHandler.NumInstances, 1);
            Assert.IsEqual(SaveDecorator1.CallCount, 1);
        }

        [Test]
        public void TestMultiple()
        {
            Container.Bind<ISaveHandler>().To<SaveHandler>().AsSingle();

            Container.Decorate<ISaveHandler>().With<SaveDecorator1>();
            Container.Decorate<ISaveHandler>().With<SaveDecorator2>();

            CallCounter = 1;
            SaveHandler.CallCount = 0;
            SaveDecorator1.CallCount = 0;
            SaveDecorator2.CallCount = 0;

            Container.Resolve<ISaveHandler>().Save();

            Assert.IsEqual(SaveDecorator2.CallCount, 1);
            Assert.IsEqual(SaveDecorator1.CallCount, 2);
            Assert.IsEqual(SaveHandler.CallCount, 3);
        }

        [Test]
        public void TestSimpleCase()
        {
            Container.Bind<ISaveHandler>().To<SaveHandler>().AsSingle();
            Container.Decorate<ISaveHandler>().With<SaveDecorator1>();

            CallCounter = 1;
            SaveHandler.CallCount = 0;
            SaveDecorator1.CallCount = 0;

            Container.Resolve<ISaveHandler>().Save();

            Assert.IsEqual(SaveDecorator1.CallCount, 1);
            Assert.IsEqual(SaveHandler.CallCount, 2);
        }

        #endregion

        // TODO - Fix this
        //[Test]
        //public void TestContainerInheritance2()
        //{
        //Container.Bind<ISaveHandler>().To<SaveHandler>().AsSingle();
        //Container.Decorate<ISaveHandler>().With<SaveDecorator1>();

        //var subContainer = Container.CreateSubContainer();
        //subContainer.Decorate<ISaveHandler>().With<SaveDecorator2>();

        //CallCounter = 1;
        //SaveHandler.CallCount = 0;
        //SaveDecorator1.CallCount = 0;
        //SaveDecorator2.CallCount = 0;

        //subContainer.Resolve<ISaveHandler>().Save();

        //Assert.IsEqual(SaveDecorator2.CallCount, 1);
        //Assert.IsEqual(SaveDecorator1.CallCount, 2);
        //Assert.IsEqual(SaveHandler.CallCount, 3);
        //}
    }
}