using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class SetAnimatorTrigger : GameCommandHandler
    {
        #region Variables

        public Animator animator;
        public string triggerName;

        #endregion

        #region Unity lifecycle

        private void Reset()
        {
            animator = GetComponent<Animator>();
        }

        #endregion

        #region Public methods

        public override void PerformInteraction()
        {
            if (animator)
            {
                animator.SetTrigger(triggerName);
            }
        }

        #endregion
    }
}