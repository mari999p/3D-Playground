using Playground.Game.Player;
using UnityEngine;

namespace Playground.Game.Block
{
    public class DangerousBlock : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Transform _blockTransform;
        [SerializeField] private Vector3 _checkPlayerSize = new(1f, 1f, 1f);
        [SerializeField] private float _sizeOffsetY;
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private PlayerDeath _playerDeath;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            CheckPlayerBeneathBlock();
        }

        private void OnDrawGizmos()
        {
            if (_blockTransform != null)
            {
                Gizmos.color = Color.blue;
                Vector3 offsetPosition = _blockTransform.position + Vector3.down * _sizeOffsetY;
                Gizmos.DrawWireCube(offsetPosition, _checkPlayerSize);
            }
        }

        #endregion

        #region Private methods

        private void CheckPlayerBeneathBlock()
        {
            Vector3 offsetPosition = _blockTransform.position + Vector3.down * _sizeOffsetY;
            if (Physics.CheckBox(offsetPosition, _checkPlayerSize / 2, Quaternion.identity, _playerLayerMask))
            {
                PlayerLose();
            }
        }

        private void PlayerLose()
        {
            Debug.Log("Player Lose");
            _playerDeath.Die();
        }

        #endregion
    }
}