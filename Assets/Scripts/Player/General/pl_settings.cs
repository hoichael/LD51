using UnityEngine;

public class pl_settings : MonoBehaviour
{
    [field: SerializeField, Header("Movement")] public SO_pl_settings_move move { get; private set; }

    [field: SerializeField, Header("Jump")] public SO_pl_settings_jump jump { get; private set; }

    [field: SerializeField, Header("Walljump")] public SO_pl_settings_walljump walljump { get; private set; }

    [field: SerializeField, Header("Gravity")] public SO_pl_settings_gravity gravity { get; private set; }

    [field: SerializeField, Header("Throw")] public SO_pl_settings_throw ballThrow { get; private set; }

    [field: SerializeField, Header("Checks")] public SO_pl_settings_checks checks { get; private set; }

    [field: SerializeField, Header("Visual")] public SO_pl_settings_visual visual { get; private set; }
}
