using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clock_manager : MonoBehaviour
{
    [SerializeField] Transform anchorHand, anchorPendulum;
    [SerializeField] float rotOuterPendulum;

    int currentRotHand = 180;

    [SerializeField] float pendMoveIterations; // basically the pendulum anim "framerate" -> amount of individual rot displacements
    [SerializeField] AnimationCurve curvePend;

    float pendInterval;
    int pendCurrentIteration;
    int pendCurrentIncr; // either 1 or -1 depending on current "direction" of pendulum

    float t0, t1, t2, t3;

    private void Start()
    {
        // set initial anchor rotations for fresh clock cycle
        anchorHand.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRotHand));
        //anchorPendulum.localRotation = Quaternion.Euler(new Vector3(0, 0, rotOuterPendulum));

        StartCoroutine(HandRoutine());

        //pendAnimnActive = true;
        pendCurrentIncr = 1;
        pendCurrentIteration = Mathf.FloorToInt(pendMoveIterations * 0.5f);
        pendInterval = 10 / pendMoveIterations;
        StartCoroutine(PendelumRoutine());

        t0 = Time.time;
    }

    private IEnumerator PendelumRoutine()
    {
        yield return new WaitForSeconds(pendInterval);

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

        StartCoroutine(PendelumRoutine());
    }

    private IEnumerator HandRoutine()
    {
        yield return new WaitForSeconds(1);

        currentRotHand -= 36;
        if (currentRotHand == -180)
        {
            currentRotHand = 180;
            //print(Time.time - t0);
            //t0 = Time.time;
        }
        anchorHand.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRotHand));
        StartCoroutine(HandRoutine());
    }
}
