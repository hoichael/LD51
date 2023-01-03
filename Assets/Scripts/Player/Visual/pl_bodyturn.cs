using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_bodyturn : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform turnContainer;

    const int fullTurnLeft = 110;

    float currentBaseRotY;
    float currentOffsetLand, currentTargetLand;

    float currentLandLerpFactor = 1;

    float currentThrowLerpfactor = 1;
    float throwLerpRotYStart, throwLerpRotYTarget;

    private void FixedUpdate()
    {
        if(currentThrowLerpfactor != 1)
        {
            HandleThrowLerp();
            return;
        }

        HandleTurn();

        if(currentLandLerpFactor != 1)
        {
            HandleLandLerp();
        }

        ApplyRot();
    }

    private void HandleTurn()
    {
        currentBaseRotY = Mathf.MoveTowards(currentBaseRotY, refs.info.dir == 1 ? 0 : fullTurnLeft, refs.settings.visual.baseTurnSpeed);
    }

    private void HandleLandLerp()
    {
        currentLandLerpFactor = Mathf.MoveTowards(currentLandLerpFactor, 1, refs.settings.visual.landDeformSpeed);

        currentOffsetLand = Mathf.Lerp(
            0,
            currentTargetLand,
            Mathf.PingPong(currentLandLerpFactor, 0.5f)
            );
    }

    public void OnLand(float velY) // called from called from pl_groundcheck
    {
        if (Mathf.Abs(velY) < refs.settings.visual.minVelYToDeformOnLand) return;

        float factor = Mathf.Clamp(Mathf.Abs(velY) - 10, 5, 75) / 75;

        float rotAdd = Mathf.Lerp(
            0,
            Mathf.Abs(refs.settings.visual.maxRotLandAddY),
            factor
            );

        currentTargetLand = refs.settings.visual.minRotLandY - rotAdd;

        currentLandLerpFactor = 0;
    }

    public void OnThrow() // called from pl_throw_manager
    {
        currentThrowLerpfactor = 0;
        currentLandLerpFactor = 1;

        if(refs.info.dir == 1)
        {
            throwLerpRotYStart = 0;
            throwLerpRotYTarget = -359;
        }
        else
        {
            throwLerpRotYStart = fullTurnLeft;
            throwLerpRotYTarget = 359;
        }
    }

    private void HandleThrowLerp()
    {
        currentThrowLerpfactor = Mathf.MoveTowards(currentThrowLerpfactor, 1, refs.settings.visual.throwTurnSpeed);

        float newRotY = Mathf.Lerp(
            throwLerpRotYStart,
            throwLerpRotYTarget,
            refs.settings.visual.throwTurnCurve.Evaluate(currentThrowLerpfactor)
            );

        if (currentThrowLerpfactor >= 0.98f)
        {
            newRotY = throwLerpRotYStart == 0 ? 0 : fullTurnLeft;
        }

        turnContainer.localRotation = Quaternion.Euler(new Vector3(0, newRotY, 0));
    }

    private void ApplyRot()
    {
        turnContainer.localRotation = Quaternion.Euler(new Vector3(0, currentBaseRotY + currentOffsetLand * refs.info.dir, 0));
    }
}
