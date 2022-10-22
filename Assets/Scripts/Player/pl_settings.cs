using UnityEngine;

public class pl_settings : MonoBehaviour
{
    [Header("Movement")]
    public float moveForceGround;
    public float moveForceAir;
    public float dragGround, dragAir;
    public float moveForceAddAirTurn;

    [Header("Jump")]
    public float jumpForceBase;
    public float jumpForceAdd;
    public float jumpAddDuration;
    public float jumpTermMult; // mult with y vel when jump terminated. needs to be between 0 and 1

    [Header("Wall Jump")]
    public Vector3 wallCheckOffset;
    public Vector2 wallCheckSize;
    public Vector2 wallJumpDir;
    public Vector2 wallJumpForce;

    [Header("Gravity")]
    public float gravBase;
    public float gravAdd;
    public float gravMax;
    public float gravBaseWallJump;

    [Header("Throw")]
    public float throwForceBase;
    public float throwForceAdd;
    public float throwForceMax;
    public float xHairClamp;
    public float xHairSens;
    public float throwIndicatorLength;
    public float throwIndicatorOffset;

    [Header("Misc")]
    public Vector2 groundcheckSize;
}
