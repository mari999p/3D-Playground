using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class StandardMaterialEmissionBehaviour : PlayableBehaviour
{
    #region Variables

    [ColorUsage(false, true)]
    public Color color;
    public int materialIndex;
    public bool materialIndicesMatch = true;

    #endregion
}