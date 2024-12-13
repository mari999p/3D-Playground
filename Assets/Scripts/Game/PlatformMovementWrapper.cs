using NaughtyAttributes;
using UnityEngine;

namespace Playground.Game
{
    public class PlatformMovementWrapper : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PlatformMovement _platformMovement1;
        [SerializeField] private PlatformMovement _platformMovement2;

        #endregion

        #region Public methods

        [Button]
        public void Play()
        {
            _platformMovement1.Move();
            _platformMovement2.MoveLinear();
        }

        #endregion
    }
}