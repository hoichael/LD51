using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/Settings/Checks", fileName = "pl_set_checks_", order = 5)]
public class SO_pl_settings_checks : ScriptableObject
{
    [field: SerializeField] public LayerMask solidLayer { get; private set; }
    [field: SerializeField] public Vector2 groundCheckSize { get; private set; }
    [field: SerializeField] public Vector2 wallCheckSize { get; private set; }
    [field: SerializeField] public Vector3 wallCheckOffset { get; private set; }
    [field: SerializeField] public float slopeCheckLength { get; private set; }
    [field: SerializeField] public float slopeCheckOffsetHor { get; private set; }
}
