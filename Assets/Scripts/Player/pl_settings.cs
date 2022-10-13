using UnityEngine;

public class pl_settings : MonoBehaviour
{
    [Header("Movement")]
    public float moveForceGround;
    public float moveForceAir;
    public float dragGround, dragAir;

    [Header("Jump")]
    public float jumpForceBase;
    public float jumpForceAdd;
    public float jumpAddDuration;
    public float jumpTermMult; // mult with y vel when jump terminated. needs to be between 0 and 1

    [Header("Gravity")]
    public float gravBase;
    public float gravAdd;
    public float gravMax;

    [Header("Misc")]
    public Vector2 groundcheckSize;
}
