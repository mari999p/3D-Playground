using System;
using UnityEngine;

namespace Zenject.Asteroids
{
    public abstract class ShipState : IDisposable
    {
        #region IDisposable

        public virtual void Dispose()
        {
            // optionally overridden
        }

        #endregion

        #region Public methods

        public virtual void OnTriggerEnter(Collider other)
        {
            // optionally overridden
        }

        public virtual void Start()
        {
            // optionally overridden
        }

        public abstract void Update();

        #endregion
    }
}