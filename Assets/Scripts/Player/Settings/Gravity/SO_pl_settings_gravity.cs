using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/Settings/Gravity", fileName = "pl_set_grav_", order = 3)]
public class SO_pl_settings_gravity : ScriptableObject
{
    [field: SerializeField] public float forceBase { get; private set; }
    [field: SerializeField] public float forceAdd { get; private set; }
    [field: SerializeField] public float forceMax { get; private set; }
}
