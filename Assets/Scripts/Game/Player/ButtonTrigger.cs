using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Playground.Game.Player
{
    public class ButtonTrigger : MonoBehaviour
    {
        #region Variables

        [Header("Block Settings")]
        [SerializeField] private Transform _blockTransform;
        [SerializeField] private float _moveDistance = 3f;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _delayBeforeReturn = 3f;

        [Header("Effect shaking")]
        [SerializeField] private float _shakeDuration = 0.5f;
        [SerializeField] private float _shakeStrength = 0.5f;
        [SerializeField] private int _shakeVibrato = 10;
        private Vector3 _initialPosition;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _initialPosition = _blockTransform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tag.Player))
            {
                StartCoroutine(LowerAndRaiseBlock());
            }
        }

        #endregion

        #region Private methods

        private IEnumerator LowerAndRaiseBlock()
        {
            yield return _blockTransform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato)
                .WaitForCompletion();
            float targetLoweredPositionY = _initialPosition.y - _moveDistance;
            while (Vector3.Distance(_blockTransform.position,
                       new Vector3(_blockTransform.position.x, targetLoweredPositionY, _blockTransform.position.z)) >
                   0.01f)
            {
                _blockTransform.position = new Vector3(_blockTransform.position.x,
                    Mathf.MoveTowards(_blockTransform.position.y, targetLoweredPositionY, _moveSpeed * Time.deltaTime),
                    _blockTransform.position.z
                );
                yield return null;
            }

            yield return new WaitForSeconds(_delayBeforeReturn);
            while (Vector3.Distance(_blockTransform.position, _initialPosition) > 0.01f)
            {
                _blockTransform.position = Vector3.MoveTowards(_blockTransform.position, _initialPosition,
                    _moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        #endregion
    }
}