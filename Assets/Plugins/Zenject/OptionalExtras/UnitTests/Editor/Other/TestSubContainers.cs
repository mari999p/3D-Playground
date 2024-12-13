using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Other
{
    [TestFixture]
    public class TestSubContainers : ZenjectUnitTestFixture
    {
        #region Public methods

        [Test]
        public void TestCase2()
        {
            Test0 test0;
            Test1 test1;

            DiContainer subContainer = Container.CreateSubContainer();
            Test0 test0Local = new Test0();

            subContainer.Bind<Test0>().FromInstance(test0Local);
            subContainer.Bind<Test1>().AsSingle();

            test0 = subContainer.Resolve<Test0>();
            Assert.IsEqual(test0Local, test0);

            test1 = subContainer.Resolve<Test1>();

            Assert.Throws(
                delegate { Container.Resolve<Test0>(); });

            Assert.Throws(
                delegate { Container.Resolve<Test1>(); });

            Container.Bind<Test0>().AsSingle();
            Container.Bind<Test1>().AsSingle();

            Assert.That(Container.Resolve<Test0>() != test0);

            Assert.That(Container.Resolve<Test1>() != test1);
        }

        [Test]
        public void TestIsRemoved()
        {
            DiContainer subContainer = Container.CreateSubContainer();
            Test0 test1 = new Test0();

            subContainer.Bind<Test0>().FromInstance(test1);

            Assert.That(ReferenceEquals(test1, subContainer.Resolve<Test0>()));

            Assert.Throws(
                delegate { Container.Resolve<Test0>(); });
        }

        [Test]
        public void TestMultipleSingletonDifferentScope()
        {
            IFoo foo1;

            DiContainer subContainer1 = Container.CreateSubContainer();
            subContainer1.Bind<IFoo>().To<Foo>().AsSingle();
            foo1 = subContainer1.Resolve<IFoo>();

            DiContainer subContainer2 = Container.CreateSubContainer();
            subContainer2.Bind<IFoo>().To<Foo>().AsSingle();
            IFoo foo2 = subContainer2.Resolve<IFoo>();

            Assert.That(foo2 != foo1);
        }

        #endregion

        #region Local data

        private class Foo : IFoo, IFoo2 { }

        private interface IFoo { }

        private interface IFoo2 { }

        private class Test0 { }

        private class Test1
        {
            #region Variables

            [Inject]
            public Test0 Test;

            #endregion
        }

        #endregion
    }
}