using UnityEngine.AI;
using UnityEngine.Playables;

public class NavMeshAgentControlMixerBehaviour : PlayableBehaviour
{
    #region Public methods

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        NavMeshAgent trackBinding = playerData as NavMeshAgent;

        if (!trackBinding)
        {
            return;
        }

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<NavMeshAgentControlBehaviour> inputPlayable =
                (ScriptPlayable<NavMeshAgentControlBehaviour>)playable.GetInput(i);
            NavMeshAgentControlBehaviour input = inputPlayable.GetBehaviour();

            if (inputWeight > 0.5f && !input.destinationSet && input.destination)
            {
                if (!trackBinding.isOnNavMesh)
                {
                    continue;
                }

                trackBinding.SetDestination(input.destination.position);
                input.destinationSet = true;
            }
        }
    }

    #endregion
}