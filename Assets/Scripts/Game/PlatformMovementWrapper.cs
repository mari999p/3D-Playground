using NaughtyAttributes;
using UnityEngine;

namespace Playground.Game
{
    public class PlatformMovementWrapper: MonoBehaviour
    {
    [SerializeField] private PlatformMovement _platformMovement1;
    [SerializeField] private PlatformMovement _platformMovement2;

    [Button]
    public void Play()
    {
        _platformMovement1.Move();
        _platformMovement2.MoveLinear();
    }
}
}