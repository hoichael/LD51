using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/Settings/Jump", fileName = "pl_set_jump_", order = 1)]
public class SO_pl_settings_jump : ScriptableObject
{
    [field: SerializeField] public float forceBase { get; private set; }
    [field: SerializeField] public float forceAdd { get; private set; }
    [field: SerializeField] public float addDuration { get; private set; }
    [field: SerializeField] public float termMult { get; private set; }
    [field: SerializeField] public float slopeForceBase { get; private set; }
    [field: SerializeField] public float slopeForceAdd { get; private set; }
}
