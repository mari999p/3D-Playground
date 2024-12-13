using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Other
{
    [TestFixture]
    public class TestKeyedFactoryExample : ZenjectUnitTestFixture
    {
        #region Public Nested Types

        public class FooFactory
        {
            #region Variables

            private readonly Dictionary<string, IFactory<Foo>> _subFactories;

            #endregion

            #region Setup/Teardown

            public FooFactory(
                Dictionary<string, IFactory<Foo>> subFactories)
            {
                _subFactories = subFactories;
            }

            #endregion

            #region Public methods

            public Foo Create(string key)
            {
                return _subFactories[key].Create();
            }

            #endregion
        }

        public class Foo
        {
            #region Public Nested Types

            public class Factory : PlaceholderFactory<Foo> { }

            #endregion

            #region Properties

            public int Number { get; private set; }

            #endregion

            #region Setup/Teardown

            public Foo(int number)
            {
                Number = number;
            }

            #endregion
        }

        #endregion

        #region Public methods

        [Test]
        public void Test1()
        {
            Container.BindFactory<Foo, Foo.Factory>().WithId("foo1")
                .FromSubContainerResolve().ByMethod(InstallFoo1);

            Container.BindFactory<Foo, Foo.Factory>().WithId("foo2")
                .FromSubContainerResolve().ByMethod(InstallFoo2);

            Container.Bind<Dictionary<string, IFactory<Foo>>>()
                .FromMethod(GetFooFactories).WhenInjectedInto<FooFactory>();

            Container.Bind<FooFactory>().AsSingle();

            FooFactory keyedFactory = Container.Resolve<FooFactory>();

            Assert.IsEqual(keyedFactory.Create("foo1").Number, 5);
            Assert.IsEqual(keyedFactory.Create("foo2").Number, 6);

            Assert.Throws(() => keyedFactory.Create("foo3"));
        }

        #endregion

        #region Private methods

        private Dictionary<string, IFactory<Foo>> GetFooFactories(InjectContext ctx)
        {
            return ctx.Container.AllContracts.Where(
                    x => x.Type == typeof(Foo.Factory))
                .ToDictionary(x => (string)x.Identifier,
                    x => (IFactory<Foo>)ctx.Container.ResolveId<Foo.Factory>(x.Identifier));
        }

        private void InstallFoo1(DiContainer subContainer)
        {
            subContainer.BindInstance(5);
            subContainer.Bind<Foo>().AsCached();
        }

        private void InstallFoo2(DiContainer subContainer)
        {
            subContainer.BindInstance(6);
            subContainer.Bind<Foo>().AsCached();
        }

        #endregion
    }
}