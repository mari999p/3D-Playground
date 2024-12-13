using UnityEngine;

namespace NaughtyAttributes.Test
{
    public class AnimatorParamTest : MonoBehaviour
    {
        #region Variables

        public Animator animator0;

        [AnimatorParam("animator0")]
        public int hash0;

        [AnimatorParam("animator0")]
        public string name0;

        public AnimatorParamNest1 nest1;

        #endregion

        #region Private methods

        [Button("Log 'hash0' and 'name0'")]
        private void TestLog()
        {
            Debug.Log($"hash0 = {hash0}");
            Debug.Log($"name0 = {name0}");
            Debug.Log($"Animator.StringToHash(name0) = {Animator.StringToHash(name0)}");
        }

        #endregion
    }

    [System.Serializable]
    public class AnimatorParamNest1
    {
        #region Variables

        public Animator animator1;

        [AnimatorParam("Animator1", AnimatorControllerParameterType.Bool)]
        public int hash1;

        [AnimatorParam("Animator1", AnimatorControllerParameterType.Float)]
        public string name1;

        public AnimatorParamNest2 nest2;

        #endregion

        #region Properties

        private Animator Animator1 => animator1;

        #endregion
    }

    [System.Serializable]
    public class AnimatorParamNest2
    {
        #region Variables

        public Animator animator2;

        [AnimatorParam("GetAnimator2", AnimatorControllerParameterType.Int)]
        public int hash1;

        [AnimatorParam("GetAnimator2", AnimatorControllerParameterType.Trigger)]
        public string name1;

        #endregion

        #region Private methods

        private Animator GetAnimator2()
        {
            return animator2;
        }

        #endregion
    }
}