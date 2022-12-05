using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_settings : MonoBehaviour
{
    [field: SerializeField, Header("Screen")] public float screenSwitchSpeed { get; private set; }
    [field: SerializeField] public AnimationCurve screenSwitchCurve { get; private set; }

    [field: SerializeField] public float camPosZ { get; private set; }
}
