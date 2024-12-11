using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Playground.Utils.Logger
{
    public static class Logger
    {
        #region Public methods

        public static void Error(this object obj, object message = null, [CallerMemberName] string memberName = default)
        {
            Debug.LogError(FormatMessage(obj.GetType(), memberName, message));
        }

        [Conditional("UNITY_EDITOR")] [Conditional("DEBUG")]
        public static void Log(this object obj, object message = null, [CallerMemberName] string memberName = default)
        {
            Debug.Log(FormatMessage(obj.GetType(), memberName, message));
        }

        #endregion

        #region Private methods

        private static string FormatMessage(Type type, string memberName, object message)
        {
            // TODO: Multitrteading
            string prefix = Application.isEditor
                ? $"[{Time.frameCount}]"
                : $"[{DateTime.Now.ToString("HH:mm:ss")} : {Time.frameCount}]";
            return $"{prefix} [{type.Name} : {memberName}] {message}";
        }

        #endregion
    }
}