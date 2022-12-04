using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/Settings/Throw", fileName = "pl_set_throw_", order = 4)]
public class SO_pl_settings_throw : ScriptableObject
{
    [field: SerializeField, Header("Charge")] public float chargeCounterAdd { get; private set; }
    [field: SerializeField] public List<float> chargeCounterBreakpoints { get; private set; }
    [field: SerializeField] public List<float> forceSteps { get; private set; }

    [field: SerializeField, Header("Visual")] public List<Color> SpotPaletteA { get; private set; }
    [field: SerializeField] public List<Color> SpotPaletteB { get; private set; }
    [field: SerializeField] public float spotOpacityDefault { get; private set; }
    [field: SerializeField] public float spotOpacityActive { get; private set; }
}
