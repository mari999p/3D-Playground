using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class NavMeshAgentControlClip : PlayableAsset, ITimelineClipAsset
{
    #region Variables

    public ExposedReference<Transform> destination;
    [HideInInspector]
    public NavMeshAgentControlBehaviour template = new();

    #endregion

    #region Properties

    public ClipCaps clipCaps => ClipCaps.None;

    #endregion

    #region Public methods

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        ScriptPlayable<NavMeshAgentControlBehaviour> playable =
            ScriptPlayable<NavMeshAgentControlBehaviour>.Create(graph, template);
        NavMeshAgentControlBehaviour clone = playable.GetBehaviour();
        clone.destination = destination.Resolve(graph.GetResolver());
        return playable;
    }

    #endregion
}