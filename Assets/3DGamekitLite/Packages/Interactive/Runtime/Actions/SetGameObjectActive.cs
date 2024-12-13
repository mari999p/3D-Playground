using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class SetGameObjectActive : GameCommandHandler
    {
        #region Variables

        public bool isEnabled = true;
        public GameObject[] targets;

        #endregion

        #region Public methods

        public override void PerformInteraction()
        {
            foreach (GameObject g in targets)
            {
                g.SetActive(isEnabled);
            }
        }

        #endregion
    }
}