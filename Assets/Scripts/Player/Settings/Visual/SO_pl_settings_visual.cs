using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/Settings/Visual", fileName = "pl_set_visual_", order = 1)]
public class SO_pl_settings_visual : ScriptableObject
{
    //[field: SerializeField, Header("General")] public float playerModelBasePosZ { get; private set; }

    [field: SerializeField, Header("Turn")] public float baseTurnSpeed { get; private set; }

    [field: SerializeField, Header("Throw")] public float throwTurnSpeed { get; private set; }
    [field: SerializeField] public AnimationCurve throwTurnCurve { get; private set; }

    [field: SerializeField, Header("Jump Deform")] public Vector3 maxDeformJump { get; private set; }
    [field: SerializeField] public Vector3 maxDeformLand { get; private set; }
    [field: SerializeField] public float minVelYToDeformOnLand { get; private set; }
    [field: SerializeField] public float minRotLandY { get; private set; }
    [field: SerializeField] public float maxRotLandAddY { get; private set; }
    [field: SerializeField] public float resetSpeed { get; private set; }
    [field: SerializeField] public float landDeformSpeed { get; private set; }
    [field: SerializeField] public Vector3 forcePadDeform { get; private set; }

    [field: SerializeField, Header("Slope Tilt")] public float bodyOffsetY { get; private set; }
    [field: SerializeField] public float bodyRotZ { get; private set; }
    [field: SerializeField] public float transitionSpeed { get; private set; }
}
