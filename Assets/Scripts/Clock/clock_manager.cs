using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clock_manager : MonoBehaviour
{
    [SerializeField] Transform anchorHand, anchorPendulum;
    [SerializeField] float rotOuterPendulum;

    bool handAnimActive;
    int currentRotHand = 180;

    private void Start()
    {
        // set initial anchor rotations for fresh clock cycle
        anchorHand.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRotHand));
        anchorPendulum.localRotation = Quaternion.Euler(new Vector3(0, 0, rotOuterPendulum));

        StartCoroutine(HandRoutine());
    }

    private void Update()
    {
        if (handAnimActive) HandleHandAnim();
    }

    private void HandleHandAnim()
    {

    }

    private IEnumerator HandRoutine()
    {
        /*
        // static for 0.8s
        yield return new WaitForSeconds(0.8f);

        // move for 0.2s. then restart routine
        handAnimActive = true;
        yield return new WaitForSeconds(0.2f);

        handAnimActive = false;
        StartCoroutine(HandRoutine());
        */

        yield return new WaitForSeconds(1);
        currentRotHand -= 36;
        if (currentRotHand == -180) currentRotHand = 180;
        anchorHand.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRotHand));
        StartCoroutine(HandRoutine());
    }
}
