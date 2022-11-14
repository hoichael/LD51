using UnityEngine;

public class pl_settings : MonoBehaviour
{
    //[Header("Movement")]
    //public float moveForceGround;
    //public float moveForceAir;
    //public float dragGround, dragAir;
    //public float groundVelResetFactor;
    //public float moveForceTurnGround;
    //public float moveForceTurnAir;
    [field: SerializeField, Header("Movement")] public SO_pl_settings_move move { get; private set; }

    //[Header("Jump")]
    //public float jumpForceBase;
    //public float jumpForceAdd;
    //public float jumpAddDuration;
    //public float jumpTermMult; // mult with y vel when jump terminated. needs to be between 0 and 1
    //public float slopeJumpForceBase;
    //public float slopeJumpForceAdd;
    [field: SerializeField, Header("Jump")] public SO_pl_settings_jump jump { get; private set; }

    //[Header("Wall Jump")]
    //public Vector3 wallCheckOffset;
    //public Vector2 wallCheckSize;
    //public Vector2 wallJumpDir;
    //public Vector2 wallJumpForce;
    //public Vector2 wallJumpForceAdd;
    //public float wallJumpAddResetSpeed;
    //public float dragWalljump;
    [field: SerializeField, Header("Walljump")] public SO_pl_settings_walljump walljump { get; private set; }

    //[Header("Gravity")]
    //public float gravBase;
    //public float gravAdd;
    //public float gravMax;
    ////public float gravBaseWallJump;
    [field: SerializeField, Header("Gravity")] public SO_pl_settings_gravity gravity { get; private set; }

    //[Header("Throw")]
    //public float throwForceBase;
    //public float throwForceAdd;
    //public float throwForceMax;
    //public float xHairClamp;
    //public float xHairSens;
    //public float throwIndicatorLength;
    //public float throwIndicatorOffset;
    [field: SerializeField, Header("Throw")] public SO_pl_settings_throw ballThrow { get; private set; }

    //[Header("Checks")]
    //public LayerMask solidLayer;
    //public Vector2 groundcheckSize;
    //public float slopeCheckLength;
    [field: SerializeField, Header("Checks")] public SO_pl_settings_checks checks { get; private set; }
}
