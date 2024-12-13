using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class PlayAnimation : GameCommandHandler
    {
        #region Variables

        public Animation[] animations;

        #endregion

        #region Public methods

        public override void PerformInteraction()
        {
            foreach (Animation a in animations)
            {
                a.Play();
            }
        }

        #endregion
    }
}