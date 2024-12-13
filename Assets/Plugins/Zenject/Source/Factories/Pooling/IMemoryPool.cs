using System;

namespace Zenject
{
    public interface IMemoryPool
    {
        #region Properties

        Type ItemType { get; }
        int NumActive { get; }
        int NumInactive { get; }
        int NumTotal { get; }

        #endregion

        #region Public methods

        void Clear();

        void Despawn(object obj);

        /// <summary>
        /// Expands the pool by the additional size.
        /// This bypasses the configured expansion method (OneAtATime or Doubling)
        /// </summary>
        /// <param name="numToAdd">The additional number of items to allocate in the pool</param>
        void ExpandBy(int numToAdd);

        /// <summary>
        /// Changes pool size by creating new elements or destroying existing elements
        /// This bypasses the configured expansion method (OneAtATime or Doubling)
        /// </summary>
        void Resize(int desiredPoolSize);

        /// <summary>
        /// Shrinks the MemoryPool by removing a given number of elements
        /// This bypasses the configured expansion method (OneAtATime or Doubling)
        /// </summary>
        /// <param name="numToRemove">The amount of items to remove from the pool</param>
        void ShrinkBy(int numToRemove);

        #endregion
    }

    public interface IDespawnableMemoryPool<TValue> : IMemoryPool
    {
        #region Public methods

        void Despawn(TValue item);

        #endregion
    }

    public interface IMemoryPool<TValue> : IDespawnableMemoryPool<TValue>
    {
        #region Public methods

        TValue Spawn();

        #endregion
    }

    public interface IMemoryPool<in TParam1, TValue> : IDespawnableMemoryPool<TValue>
    {
        #region Public methods

        TValue Spawn(TParam1 param);

        #endregion
    }

    public interface IMemoryPool<in TParam1, in TParam2, TValue> : IDespawnableMemoryPool<TValue>
    {
        #region Public methods

        TValue Spawn(TParam1 param1, TParam2 param2);

        #endregion
    }

    public interface IMemoryPool<in TParam1, in TParam2, in TParam3, TValue> : IDespawnableMemoryPool<TValue>
    {
        #region Public methods

        TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3);

        #endregion
    }

    public interface
        IMemoryPool<in TParam1, in TParam2, in TParam3, in TParam4, TValue> : IDespawnableMemoryPool<TValue>
    {
        #region Public methods

        TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);

        #endregion
    }

    public interface
        IMemoryPool<in TParam1, in TParam2, in TParam3, in TParam4, in TParam5, TValue> : IDespawnableMemoryPool<TValue>
    {
        #region Public methods

        TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);

        #endregion
    }

    public interface
        IMemoryPool<in TParam1, in TParam2, in TParam3, in TParam4, in TParam5, in TParam6,
            TValue> : IDespawnableMemoryPool<TValue>
    {
        #region Public methods

        TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6);

        #endregion
    }

    public interface IMemoryPool<in TParam1, in TParam2, in TParam3, in TParam4, in TParam5, in TParam6, in TParam7,
        TValue> : IDespawnableMemoryPool<TValue>
    {
        #region Public methods

        TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6,
            TParam7 param7);

        #endregion
    }

    public interface IMemoryPool<in TParam1, in TParam2, in TParam3, in TParam4, in TParam5, in TParam6, in TParam7,
        in TParam8, TValue> : IDespawnableMemoryPool<TValue>
    {
        #region Public methods

        TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6,
            TParam7 param7, TParam8 param8);

        #endregion
    }
}