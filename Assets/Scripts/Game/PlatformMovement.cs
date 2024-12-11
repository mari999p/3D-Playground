using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Playground.Game
{
    public class PlatformMovement : MonoBehaviour
    {
        #region Variables

        private Tween _tween;

        #endregion

        #region Unity lifecycle

        private void OnDestroy()
        {
            Kill();
        }

        #endregion

        #region Public methods

        [Button]
        public void Move()
        {
            Kill();
            _tween = transform
                .DOMoveX(2, 5f)
                .From(-10)
                .OnComplete(() => Debug.LogError("COMPLETED!"))
                .OnKill(() => Debug.LogError("KILL!"));
        }

        [Button]
        public void MoveLinear()
        {
            Kill();
            _tween = transform
                .DOMoveX(2, 5f)
                .From(-10)
                .SetEase(Ease.Linear)
                .OnComplete(() => Debug.LogError("COMPLETED!"))
                .OnKill(() => Debug.LogError("KILL!"));
        }

        #endregion

        #region Private methods

        [Button]
        private void Kill()
        {
            _tween?.Kill();
        }

        #endregion
    }
}