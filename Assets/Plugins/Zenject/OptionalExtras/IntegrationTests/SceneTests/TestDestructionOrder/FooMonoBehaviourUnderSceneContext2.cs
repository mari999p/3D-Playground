using UnityEngine;

namespace Zenject.Tests.TestDestructionOrder
{
    public class FooMonoBehaviourUnderSceneContext2 : MonoBehaviour
    {
        #region Unity lifecycle

        public void OnDestroy()
        {
            Debug.Log("Destroyed FooMonoBehaviourUnderSceneContext2");
        }

        #endregion
    }
}