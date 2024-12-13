using UnityEngine;

namespace Zenject.Tests.TestDestructionOrder
{
    public class FooMonoBehaviourUnderSceneContext1 : MonoBehaviour
    {
        #region Unity lifecycle

        public void OnDestroy()
        {
            Debug.Log("Destroyed FooMonoBehaviourUnderSceneContext1");
        }

        #endregion
    }
}