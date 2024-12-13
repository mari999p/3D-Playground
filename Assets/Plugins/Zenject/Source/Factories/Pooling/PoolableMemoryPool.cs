namespace Zenject
{
    // Zero parameters
    public class PoolableMemoryPool<TValue>
        : MemoryPool<TValue>
        where TValue : IPoolable
    {
        #region Protected methods

        protected override void OnDespawned(TValue item)
        {
            item.OnDespawned();
        }

        protected override void Reinitialize(TValue item)
        {
            item.OnSpawned();
        }

        #endregion
    }

    // One parameters
    public class PoolableMemoryPool<TParam1, TValue>
        : MemoryPool<TParam1, TValue>
        where TValue : IPoolable<TParam1>
    {
        #region Protected methods

        protected override void OnDespawned(TValue item)
        {
            item.OnDespawned();
        }

        protected override void Reinitialize(TParam1 p1, TValue item)
        {
            item.OnSpawned(p1);
        }

        #endregion
    }

    // Two parameters
    public class PoolableMemoryPool<TParam1, TParam2, TValue>
        : MemoryPool<TParam1, TParam2, TValue>
        where TValue : IPoolable<TParam1, TParam2>
    {
        #region Protected methods

        protected override void OnDespawned(TValue item)
        {
            item.OnDespawned();
        }

        protected override void Reinitialize(TParam1 p1, TParam2 p2, TValue item)
        {
            item.OnSpawned(p1, p2);
        }

        #endregion
    }

    // Three parameters
    public class PoolableMemoryPool<TParam1, TParam2, TParam3, TValue>
        : MemoryPool<TParam1, TParam2, TParam3, TValue>
        where TValue : IPoolable<TParam1, TParam2, TParam3>
    {
        #region Protected methods

        protected override void OnDespawned(TValue item)
        {
            item.OnDespawned();
        }

        protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TValue item)
        {
            item.OnSpawned(p1, p2, p3);
        }

        #endregion
    }

    // Four parameters
    public class PoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>
        : MemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>
        where TValue : IPoolable<TParam1, TParam2, TParam3, TParam4>
    {
        #region Protected methods

        protected override void OnDespawned(TValue item)
        {
            item.OnDespawned();
        }

        protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TValue item)
        {
            item.OnSpawned(p1, p2, p3, p4);
        }

        #endregion
    }

    // Five parameters
    public class PoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>
        : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>
        where TValue : IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5>
    {
        #region Protected methods

        protected override void OnDespawned(TValue item)
        {
            item.OnDespawned();
        }

        protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TValue item)
        {
            item.OnSpawned(p1, p2, p3, p4, p5);
        }

        #endregion
    }

    // Six parameters
    public class PoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>
        : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>
        where TValue : IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>
    {
        #region Protected methods

        protected override void OnDespawned(TValue item)
        {
            item.OnDespawned();
        }

        protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6,
            TValue item)
        {
            item.OnSpawned(p1, p2, p3, p4, p5, p6);
        }

        #endregion
    }

    // Seven parameters
    public class PoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>
        : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>
        where TValue : IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>
    {
        #region Protected methods

        protected override void OnDespawned(TValue item)
        {
            item.OnDespawned();
        }

        protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6,
            TParam7 p7, TValue item)
        {
            item.OnSpawned(p1, p2, p3, p4, p5, p6, p7);
        }

        #endregion
    }

    // Eight parameters
    public class PoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>
        : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>
        where TValue : IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>
    {
        #region Protected methods

        protected override void OnDespawned(TValue item)
        {
            item.OnDespawned();
        }

        protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6,
            TParam7 p7, TParam8 p8, TValue item)
        {
            item.OnSpawned(p1, p2, p3, p4, p5, p6, p7, p8);
        }

        #endregion
    }
}