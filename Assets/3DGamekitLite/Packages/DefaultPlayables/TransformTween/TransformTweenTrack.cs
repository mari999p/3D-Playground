using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.855f, 0.8623f, 0.870f)]
[TrackClipType(typeof(TransformTweenClip))]
[TrackBindingType(typeof(Transform))]
public class TransformTweenTrack : TrackAsset
{
    #region Public methods

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TransformTweenMixerBehaviour>.Create(graph, inputCount);
    }

    public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        Transform comp = director.GetGenericBinding(this) as Transform;
        if (comp == null)
        {
            return;
        }

        SerializedObject so = new UnityEditor.SerializedObject(comp);
        SerializedProperty iter = so.GetIterator();
        while (iter.NextVisible(true))
        {
            if (iter.hasVisibleChildren)
            {
                continue;
            }

            driver.AddFromName<Transform>(comp.gameObject, iter.propertyPath);
        }
#endif
        base.GatherProperties(director, driver);
    }

    #endregion
}