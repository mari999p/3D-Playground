using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class NavMeshAgentControlBehaviour : PlayableBehaviour
{
    #region Variables

    public Transform destination;
    public bool destinationSet;

    #endregion

    #region Public methods

    public override void OnGraphStart(Playable playable)
    {
        destinationSet = false;
    }

    #endregion
}