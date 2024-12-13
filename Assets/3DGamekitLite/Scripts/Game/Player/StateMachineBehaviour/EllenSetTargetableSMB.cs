using UnityEngine;

namespace Gamekit3D
{
    public class EllenSetTargetableSMB : StateMachineBehaviour
    {
        #region Unity lifecycle

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerController controller = animator.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.RespawnFinished();
            }
        }

        #endregion
    }
}