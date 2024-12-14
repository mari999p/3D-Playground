using DG.Tweening;
using UnityEngine;

namespace Playground.Game.Block
{
    public class FallingBlock : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _fallDelay = 0.5f;
        [SerializeField] private float _fallDistance = 3f;
        [SerializeField] private float _fallDuration = 1f;
        [SerializeField] private float _returnDuration = 1f;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private bool _canFall = true;
        private Vector3 _initialPosition;
        private bool _isFalling;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _initialPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isFalling && _canFall && ((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                _isFalling = true;
                Invoke(nameof(StartFalling), _fallDelay);
            }
        }

        #endregion

        #region Private methods

        private void ReturnToInitialPosition()
        {
            transform.DOMoveY(_initialPosition.y, _returnDuration).OnComplete(() => { _isFalling = false; });
        }

        private void StartFalling()
        {
            if (_isFalling)
            {
                Sequence fallSequence = DOTween.Sequence();
                fallSequence.Append(transform.DOMoveY(transform.position.y - _fallDistance, _fallDuration))
                    .AppendInterval(_returnDuration)
                    .OnComplete(ReturnToInitialPosition);
            }
        }

        #endregion
    }
}