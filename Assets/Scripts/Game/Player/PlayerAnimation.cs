using UnityEngine;

namespace Playground.Game.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        #region Variables

        private static readonly int Death = Animator.StringToHash("death");
        [SerializeField] private Animator _animator;

        #endregion

        #region Public methods

        public void TriggerDeath()
        {
            _animator.SetTrigger(Death);
        }

        #endregion
    }
}