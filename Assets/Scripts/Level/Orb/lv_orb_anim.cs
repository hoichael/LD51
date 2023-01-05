using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv_orb_anim : MonoBehaviour
{
    [SerializeField] float animSpeed;
    [SerializeField] AnimationCurve animCurve;

    float currentLerpFactor;

    private void OnEnable()
    {
        currentLerpFactor = 0;
    }

    private void FixedUpdate()
    {
        HandleAnim();
    }

    private void HandleAnim()
    {
        currentLerpFactor = Mathf.MoveTowards(currentLerpFactor, 1, animSpeed);

        transform.localScale = Vector3.Lerp(
            new Vector3(0, 0, 0),
            new Vector3(2, 2, 2),
            animCurve.Evaluate(currentLerpFactor)
            );

        if (currentLerpFactor == 1)
        {
            this.enabled = false;
        }
    }
}
