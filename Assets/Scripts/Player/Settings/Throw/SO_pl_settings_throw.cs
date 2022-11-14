using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/Settings/Throw", fileName = "pl_set_throw_", order = 4)]
public class SO_pl_settings_throw : ScriptableObject
{
    [field: SerializeField, Header("Force")] public float forceBase { get; private set; }
    [field: SerializeField] public float forceAdd { get; private set; }
    [field: SerializeField] public float forceMax { get; private set; }
    [field: SerializeField, Header("Visual")] public float xHairOffset { get; private set; }
    //[field: SerializeField] public float indicatorLength { get; private set; }
    //[field: SerializeField] public float indicatorOffset { get; private set; }
}
