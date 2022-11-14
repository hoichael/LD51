using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/Settings/Move", fileName = "pl_set_move_", order = 0)]
public class SO_pl_settings_move : ScriptableObject
{
    [field: SerializeField, Header("Ground")] public float forceGround { get; private set; }
    [field: SerializeField] public float turnForceGround { get; private set; }
    [field: SerializeField] public float groundVelResetFactor { get; private set; }
    [field: SerializeField] public float dragGround { get; private set; }
    [field: SerializeField, Header("Air")] public float forceAir { get; private set; }
    [field: SerializeField] public float turnForceAir { get; private set; }
    [field: SerializeField] public float dragAir { get; private set; }
}
