using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class GameplayCounter : GameCommandHandler
    {
        #region Variables

        public int currentCount;
        [Tooltip("Perform an action when increment is performed. (optional)")]
        public GameCommandHandler onIncrementPerformAction;

        [Space]
        [Tooltip("Send a command when increment is performed. (optional)")]
        public SendGameCommand onIncrementSendCommand;
        [Tooltip("Perform an action when target count is reacted. (optional)")]
        public GameCommandHandler onTargetReachedPerformAction;
        [Space]
        [Tooltip("Send a command when target count is reacted. (optional)")]
        public SendGameCommand onTargetReachedSendCommand;
        public int targetCount = 3;

        #endregion

        #region Public methods

        public override void PerformInteraction()
        {
            currentCount += 1;
            if (currentCount >= targetCount)
            {
                if (onTargetReachedPerformAction != null)
                {
                    onTargetReachedPerformAction.PerformInteraction();
                }

                if (onTargetReachedSendCommand != null)
                {
                    onTargetReachedSendCommand.Send();
                }

                isTriggered = true;
            }
            else
            {
                if (onIncrementPerformAction != null)
                {
                    onIncrementPerformAction.PerformInteraction();
                }

                if (onIncrementSendCommand != null)
                {
                    onIncrementSendCommand.Send();
                }

                isTriggered = false;
            }
        }

        #endregion
    }
}