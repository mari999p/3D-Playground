using UnityEngine;

namespace Playground.Game.Player
{
    public abstract class PlayerBehaviour : MonoBehaviour
    {
        #region Public methods

        public void Activate()
        {
            enabled = true;
        }

        public void Deactivate()
        {
            enabled = false;
        }

        #endregion
    }
}