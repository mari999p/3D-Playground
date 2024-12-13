using System;
using System.Collections;
using System.Collections.Generic;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DropdownAttribute : DrawerAttribute
    {
        #region Properties

        public string ValuesName { get; private set; }

        #endregion

        #region Setup/Teardown

        public DropdownAttribute(string valuesName)
        {
            ValuesName = valuesName;
        }

        #endregion
    }

    public interface IDropdownList : IEnumerable<KeyValuePair<string, object>> { }

    public class DropdownList<T> : IDropdownList
    {
        #region Variables

        private readonly List<KeyValuePair<string, object>> _values;

        #endregion

        #region Setup/Teardown

        public DropdownList()
        {
            _values = new List<KeyValuePair<string, object>>();
        }

        #endregion

        #region IDropdownList

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Public methods

        public static explicit operator DropdownList<object>(DropdownList<T> target)
        {
            DropdownList<object> result = new();
            foreach (KeyValuePair<string, object> kvp in target)
            {
                result.Add(kvp.Key, kvp.Value);
            }

            return result;
        }

        public void Add(string displayName, T value)
        {
            _values.Add(new KeyValuePair<string, object>(displayName, value));
        }

        #endregion
    }
}