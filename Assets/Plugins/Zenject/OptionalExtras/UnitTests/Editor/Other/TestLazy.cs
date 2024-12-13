using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Bindings.Singletons
{
    [TestFixture]
    public class TestLazy : ZenjectUnitTestFixture
    {
        #region Public Nested Types

        public class Bar
        {
            #region Variables

            public static int InstanceCount;

            #endregion

            #region Setup/Teardown

            public Bar()
            {
                InstanceCount++;
            }

            #endregion

            #region Public methods

            public void DoIt() { }

            #endregion
        }

        public class Foo
        {
            #region Variables

            private readonly LazyInject<Bar> _bar;

            #endregion

            #region Setup/Teardown

            public Foo(LazyInject<Bar> bar)
            {
                _bar = bar;
            }

            #endregion

            #region Public methods

            public void DoIt()
            {
                _bar.Value.DoIt();
            }

            #endregion
        }

        public class Qux
        {
            #region Variables

            [Inject(Optional = true)]
            public LazyInject<Bar> Bar;

            #endregion
        }

        public class Gorp
        {
            #region Variables

            public LazyInject<Bar> Bar;

            #endregion
        }

        #endregion

        #region Public methods

        [Test]
        public void Test1()
        {
            Bar.InstanceCount = 0;

            Container.Bind<Bar>().AsSingle();
            Container.Bind<Foo>().AsSingle();

            Foo foo = Container.Resolve<Foo>();

            Assert.IsEqual(Bar.InstanceCount, 0);

            foo.DoIt();

            Assert.IsEqual(Bar.InstanceCount, 1);
        }

        [Test]
        public void TestOptional1()
        {
            Container.Bind<Bar>().AsSingle();
            Container.Bind<Qux>().AsSingle();

            Assert.IsNotNull(Container.Resolve<Qux>().Bar.Value);
        }

        [Test]
        public void TestOptional2()
        {
            Container.Bind<Qux>().AsSingle();

            Assert.IsNull(Container.Resolve<Qux>().Bar.Value);
        }

        [Test]
        public void TestOptional3()
        {
            Container.Bind<Gorp>().AsSingle();

            Gorp gorp = Container.Resolve<Gorp>();
            object temp;
            Assert.Throws(() => temp = gorp.Bar.Value);
        }

        #endregion
    }
}