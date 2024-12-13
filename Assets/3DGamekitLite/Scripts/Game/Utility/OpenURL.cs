using UnityEngine;

namespace Gamekit3D
{
    public class OpenURL : MonoBehaviour
    {
        #region Variables

        public string websiteAddress;

        #endregion

        #region Public methods

        public void OpenURLOnClick()
        {
            Application.OpenURL(websiteAddress);
        }

        #endregion
    }
}