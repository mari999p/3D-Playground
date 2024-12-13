using UnityEngine;

namespace Gamekit3D
{
    public class SetCursorMode : MonoBehaviour
    {
        #region Variables

        public CursorLockMode lockMode = CursorLockMode.None;
        public bool visible = true;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            Cursor.visible = visible;
            Cursor.lockState = lockMode;
        }

        #endregion
    }
}