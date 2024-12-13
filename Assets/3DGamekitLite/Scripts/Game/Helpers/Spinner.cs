using UnityEngine;

namespace Gamekit3D
{
    public class Spinner : MonoBehaviour
    {
        #region Variables

        public Vector3 axis = Vector3.up;
        public float speed = 1;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            transform.Rotate(axis, speed * Time.deltaTime);
        }

        #endregion
    }
}