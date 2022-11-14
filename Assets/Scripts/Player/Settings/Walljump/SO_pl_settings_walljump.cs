using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/Settings/Walljump", fileName = "pl_set_walljump_", order = 2)]
public class SO_pl_settings_walljump : ScriptableObject
{
    [field: SerializeField] public Vector2 dir { get; private set; }
    [field: SerializeField] public Vector2 forceBase { get; private set; }
    [field: SerializeField] public Vector2 forceAdd { get; private set; }
    [field: SerializeField] public float addForceResetSpeed { get; private set; }
    [field: SerializeField] public float drag { get; private set; }
    [field: SerializeField] public float grav { get; private set; }
}
