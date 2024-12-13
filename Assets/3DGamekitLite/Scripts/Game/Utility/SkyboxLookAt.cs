using UnityEngine;

namespace Gamekit3D
{
    public class SkyboxLookAt : MonoBehaviour
    {
        #region Variables

        public Transform target;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            transform.LookAt(target);
        }

        #endregion
    }
}