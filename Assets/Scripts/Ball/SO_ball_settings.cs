using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ball/Settings", fileName = "ball_settings", order = 5)]
public class SO_ball_settings : ScriptableObject
{
    [field: SerializeField, Header("Rigidbody")] public float rbGravityScale { get; private set; }
    [field: SerializeField] public PhysicsMaterial2D rbPhMat { get; private set; }
    [field: SerializeField] public RigidbodyInterpolation2D rbInterp { get; private set; }
    [field: SerializeField] public CollisionDetectionMode2D rbColDetectMode { get; private set; }

    [field: SerializeField, Header("Visual")] public float rotSpeedMult { get; private set; }
    [field: SerializeField] public Color circleColorDefault { get; private set; }
    [field: SerializeField] public Color trailColorDefault { get; private set; }
    [field: SerializeField] public Color[] chargeStepColors { get; private set; }

    [field: SerializeField] public float[] colorLerpSpeedSteps { get; private set; }

    [field: SerializeField] public float velMagTrailCutoff { get; private set; }
    [field: SerializeField] public float trailInterpSpeed { get; private set; }

    [field: SerializeField, Header("General")] public float colEnableDelay { get; private set; }
}
