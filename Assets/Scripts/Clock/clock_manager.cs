using UnityEngine;

public class clock_manager : MonoBehaviour
{
    [SerializeField] light_manager lightManager;

    [SerializeField] Transform anchorHand, anchorPendulum;
    [SerializeField] float rotOuterPendulum;

    [SerializeField] AnimationCurve curveHand;
    [SerializeField] float handMoveSpeed;
    float handLerpVal = 1;
    float handCurrent, handFrom, handTo;

    [SerializeField] float pendMoveIterations; // basically the pendulum anim "framerate" -> amount of individual rot displacements
    [SerializeField] AnimationCurve curvePend;

    float pendInterval;
    int pendCurrentIteration;
    int pendCurrentIncr; // either 1 or -1 depending on current "direction" of pendulum

    float t0;

    int secCounter;

    private void Start()
    {
        //Application.targetFrameRate = 60;
        t0 = Time.time;

        // set initial anchor rotations for fresh clock cycle
        anchorHand.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        handFrom = handCurrent = 180;
        handTo = -134;
        //anchorPendulum.localRotation = Quaternion.Euler(new Vector3(0, 0, rotOuterPendulum));

        //StartCoroutine(HandRoutine());
        InvokeRepeating("HandleHandLol", 0.925f, 1);

        //pendAnimnActive = true;
        pendCurrentIncr = 1;
        pendCurrentIteration = Mathf.FloorToInt(pendMoveIterations * 0.5f);
        pendInterval = 10 / pendMoveIterations;
        //StartCoroutine(PendelumRoutine());
        InvokeRepeating("HandlePendulum", 0, pendInterval);

        InvokeRepeating("ActualTimer", 1, 1);

    }

    private void HandlePendulum()
    {
        pendCurrentIteration += pendCurrentIncr;

        float t = pendCurrentIteration / pendMoveIterations;

        anchorPendulum.localRotation = Quaternion.Lerp(
            Quaternion.Euler(new Vector3(0, 0, rotOuterPendulum)),
            Quaternion.Euler(new Vector3(0, 0, -rotOuterPendulum)),
            curvePend.Evaluate(t)
            );

        // if cycle finished flip direction
        if (pendCurrentIteration >= pendMoveIterations || pendCurrentIteration <= 0)
        {
            pendCurrentIncr *= -1;
            print(Time.time - t0);
            t0 = Time.time;
        }
    }

    private void Update()
    {
        if(handLerpVal < 1) MoveHand();
    }

    private void ActualTimer()
    {
        secCounter++;

        if(secCounter == 10)
        {
            // init foreground light flash
            lightManager.InitFlash();

            secCounter = 0;
        }
    }

    private void HandleHandLol()
    {
        handFrom = handCurrent;
        handTo = handCurrent - 36;
        handLerpVal = 0;
    }

    private void MoveHand()
    {
        handLerpVal = Mathf.MoveTowards(handLerpVal, 1, handMoveSpeed * Time.deltaTime);

        handCurrent = Mathf.Lerp(
            handFrom,
            handTo,
            curveHand.Evaluate(handLerpVal)
            );

        anchorHand.localRotation = Quaternion.Euler(0, 0, handCurrent);
    }
}
