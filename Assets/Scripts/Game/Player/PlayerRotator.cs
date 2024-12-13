using Playground.Services.Input;
using UnityEngine;
using Zenject;

namespace Playground.Game.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _sensitivity = 1f;

        private InputService _inputService;
        private float _previousPositionX;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(InputService inputService)
        {
            _inputService = inputService;
        }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _previousPositionX = _inputService.MousePosition.x;
        }

        private void Update()
        {
            float currentPositionX = _inputService.MousePosition.x;
            float delta = currentPositionX - _previousPositionX;

            transform.Rotate(Vector3.up, delta * _sensitivity);

            _previousPositionX = currentPositionX;
        }

        #endregion
    }
}