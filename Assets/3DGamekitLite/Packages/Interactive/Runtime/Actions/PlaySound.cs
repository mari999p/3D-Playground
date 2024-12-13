using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class PlaySound : GameCommandHandler
    {
        #region Variables

        public AudioSource[] audioSources;

        #endregion

        #region Public methods

        public override void PerformInteraction()
        {
            foreach (AudioSource a in audioSources)
            {
                a.Play();
            }
        }

        #endregion
    }
}