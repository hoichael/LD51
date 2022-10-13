using UnityEngine;

public class ball_settings : MonoBehaviour
{
    [Header("General")]
    public float throwForceBase;
    public float colEnableDelay;

    [Header("Rigidbody")]
    public float rbGravityScale;
    public RigidbodyInterpolation2D rbInterp;
    public CollisionDetectionMode2D rbColDetectMode;
}
