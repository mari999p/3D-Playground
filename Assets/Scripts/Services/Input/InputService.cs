using System;
using UnityEngine;

namespace Playground.Services.Input
{
    public class InputService : MonoBehaviour
    {
        #region Events

        public event Action OnJumpClicked;

        #endregion

        #region Properties

        public Vector2 Axis => new(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        public bool IsJump => UnityEngine.Input.GetButtonDown("Jump");

        public bool IsRun => UnityEngine.Input.GetButton("Fire3");
        public Vector3 MousePosition => UnityEngine.Input.mousePosition;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            if (IsJump)
            {
                OnJumpClicked?.Invoke();
            }
        }

        #endregion
    }
}