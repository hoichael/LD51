using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_manager : MonoBehaviour
{
    [SerializeField] Light spotLight, pointLight;
    [SerializeField] float animSpeed;
    [SerializeField] AnimationCurve animCurve;
    float currentAnimVal = 1;

    float intensitySpotDef, intensityPointDef;

    private void Start()
    {
        intensitySpotDef = spotLight.intensity;
        intensityPointDef = pointLight.intensity;
        spotLight.intensity = pointLight.intensity = 0;
    }

    public void InitFlash()
    {
        spotLight.intensity = intensitySpotDef;
        pointLight.intensity = intensityPointDef;

        //currentAnimVal = 0;
        StartCoroutine(FlashDelay());
    }

    private void Update()
    {
        if (currentAnimVal != 1) HandleFlashAnim();
    }

    private void HandleFlashAnim()
    {
        currentAnimVal = Mathf.MoveTowards(currentAnimVal, 1, animSpeed * Time.deltaTime);

        spotLight.intensity = Mathf.Lerp(
            intensitySpotDef,
            0,
            animCurve.Evaluate(currentAnimVal)
            );

        pointLight.intensity = Mathf.Lerp(
            intensityPointDef,
            0,
            animCurve.Evaluate(currentAnimVal)
            );
    }

    private IEnumerator FlashDelay()
    {
        yield return new WaitForSeconds(0.15f);

        currentAnimVal = 0;
    }
}
