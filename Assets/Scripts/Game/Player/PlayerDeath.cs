using UnityEngine;

namespace Playground.Game.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerAnimation _animation;

        #endregion

        #region Public methods

        public void Die()
        {
            _movement.Deactivate();
            _animation.TriggerDeath();
        }

        #endregion
    }
}