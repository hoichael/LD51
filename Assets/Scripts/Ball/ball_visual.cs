using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_visual : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    [SerializeField] Transform rotContainer;
    Vector3 rotDir = new Vector3(0, 0, 1);

    //[SerializeField] SpriteRenderer circleSpriteRenderer;
    [SerializeField] MeshRenderer mRenderer;
    Material mat;

    [SerializeField] TrailRenderer trail;

    bool currentlyThrown;

    float currentLerpFactor = 1;
    float currentLerpSpeed;

    Color currentLerpOriginCircle, currentLerpOriginTrail;

    private void Awake()
    {
        mat = mRenderer.material;
        mat.color = refs.settings.circleColorDefault;
    }

    public void HandlePickup()
    {
        currentlyThrown = false;
        rotContainer.localRotation = Quaternion.Euler(new Vector3(0, 55, 0));
    }

    public void Reset()
    {
        currentlyThrown = false;
        currentLerpFactor = 1;

        rotContainer.localRotation = Quaternion.Euler(Vector3.zero);

        //circleSpriteRenderer.color = refs.settings.circleColorDefault;
        mat.color = refs.settings.circleColorDefault;

        trail.startColor = trail.endColor = refs.settings.trailColorDefault;
    }

    public void InitThrow(int chargeStep)
    {
        refs.rb.transform.localScale = Vector3.one;
        refs.trans.localRotation = rotContainer.localRotation = Quaternion.Euler(Vector3.zero);

        currentlyThrown = true;
        currentLerpFactor = 0;
        currentLerpSpeed = refs.settings.colorLerpSpeedSteps[chargeStep];

        currentLerpOriginCircle = refs.settings.chargeStepColors[chargeStep];
        currentLerpOriginTrail = chargeStep != 0 ? refs.settings.chargeStepColors[chargeStep - 1] : refs.settings.circleColorDefault;

        trail.time = refs.rb.velocity.magnitude < refs.settings.velMagTrailCutoff ? 0 : refs.rb.velocity.magnitude * 0.01f;
    }

    private void Update()
    {
        if(currentlyThrown)
        {
            RotateSprite();
            HandleTrail();
        }

        if(currentLerpFactor != 1)
        {
            HandleColorLerp();
        }
    }

    private void HandleColorLerp()
    {
        currentLerpFactor = Mathf.MoveTowards(currentLerpFactor, 1, currentLerpSpeed * Time.deltaTime);

        //circleSpriteRenderer.color = Color.Lerp(
        mat.color = Color.Lerp(
            currentLerpOriginCircle,
            refs.settings.circleColorDefault,
            currentLerpFactor
            );

        trail.startColor = Color.Lerp(
            currentLerpOriginTrail,
            refs.settings.trailColorDefault,
            currentLerpFactor
            );
    }

    private void HandleTrail()
    {
        float targetTrailTime = refs.rb.velocity.magnitude < refs.settings.velMagTrailCutoff ? 0 : refs.rb.velocity.magnitude * 0.01f;
        trail.time = Mathf.MoveTowards(trail.time, targetTrailTime, refs.settings.trailInterpSpeed * Time.deltaTime);
    }

    private void RotateSprite()
    {
        rotContainer.Rotate((rotDir * GetRotSpeed()) * Time.deltaTime);
    }

    private float GetRotSpeed()
    {
        //return -refs.rb.velocity.x * 27.5f;
        return -refs.rb.velocity.x * refs.settings.rotSpeedMult;
    }

    private void OnDisable()
    {
        Reset();
    }
}
