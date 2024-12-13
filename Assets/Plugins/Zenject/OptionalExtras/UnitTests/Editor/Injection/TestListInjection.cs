using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Injection
{
    [TestFixture]
    public class TestListInjection : ZenjectUnitTestFixture
    {
        #region Public methods

        [Test]
        public void TestArrays()
        {
            BindListItems();
            Container.Bind<Test5>().AsSingle();
            TestListItems(Container.Resolve<Test5>().Values.ToList());
        }

        [Test]
        public void TestConstructor1()
        {
            BindListItems();
            Container.Bind<Test1>().AsSingle();
            TestListItems(Container.Resolve<Test1>().Values);
        }

        [Test]
        public void TestField1()
        {
            BindListItems();
            Container.Bind<Test3>().AsSingle();
            TestListItems(Container.Resolve<Test3>().Values);
        }

        [Test]
        public void TestIEnumerable()
        {
            BindListItems();
            Container.Bind<Test4>().AsSingle();
            TestListItems(Container.Resolve<Test4>().Values.ToList());
        }

        [Test]
        public void TestIList()
        {
            BindListItems();
            Container.Bind<Test2>().AsSingle();
            TestListItems(Container.Resolve<Test2>().Values.ToList());
        }

        #endregion

        #region Private methods

        private void BindListItems()
        {
            Container.BindInstance("foo");
            Container.BindInstance("bar");
        }

        private void TestListItems(List<string> values)
        {
            Assert.IsEqual(values[0], "foo");
            Assert.IsEqual(values[1], "bar");
        }

        #endregion

        #region Local data

        private class Test1
        {
            #region Properties

            public List<string> Values { get; private set; }

            #endregion

            #region Setup/Teardown

            public Test1(List<string> values)
            {
                Values = values;
            }

            #endregion
        }

        private class Test2
        {
            #region Properties

            public IList<string> Values { get; private set; }

            #endregion

            #region Setup/Teardown

            public Test2(IList<string> values)
            {
                Values = values;
            }

            #endregion
        }

        private class Test3
        {
            #region Variables

            [Inject]
            public List<string> Values;

            #endregion
        }

        private class Test4
        {
            #region Properties

            public IEnumerable<string> Values { get; private set; }

            #endregion

            #region Setup/Teardown

            public Test4(IEnumerable<string> values)
            {
                Values = values;
            }

            #endregion
        }

        private class Test5
        {
            #region Properties

            public string[] Values { get; private set; }

            #endregion

            #region Setup/Teardown

            public Test5(string[] values)
            {
                Values = values;
            }

            #endregion
        }

        #endregion
    }
}