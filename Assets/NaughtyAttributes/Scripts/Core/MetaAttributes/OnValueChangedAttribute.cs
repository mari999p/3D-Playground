using System;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class OnValueChangedAttribute : MetaAttribute
    {
        #region Properties

        public string CallbackName { get; private set; }

        #endregion

        #region Setup/Teardown

        public OnValueChangedAttribute(string callbackName)
        {
            CallbackName = callbackName;
        }

        #endregion
    }
}