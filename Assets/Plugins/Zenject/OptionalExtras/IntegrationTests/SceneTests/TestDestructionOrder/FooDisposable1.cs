﻿using System;
using UnityEngine;

namespace Zenject.Tests.TestDestructionOrder
{
    public class FooDisposable1 : IDisposable
    {
        #region IDisposable

        public void Dispose()
        {
            Debug.Log("Destroyed FooDisposable1");
        }

        #endregion
    }
}