namespace Zenject
{
    public enum PoolExpandMethods
    {
        OneAtATime,
        Double,
        Disabled,
    }

    [NoReflectionBaking]
    public class MemoryPoolBindInfo
    {
        #region Properties

        public PoolExpandMethods ExpandMethod { get; set; }

        public int InitialSize { get; set; }

        public int MaxSize { get; set; }

        #endregion

        #region Setup/Teardown

        public MemoryPoolBindInfo()
        {
            ExpandMethod = PoolExpandMethods.OneAtATime;
            MaxSize = int.MaxValue;
        }

        #endregion
    }
}