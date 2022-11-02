using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_throw : pl_throw_base
{
    [SerializeField] float deadThreshold = 0.6f;
    Vector2 currentInput;
    [SerializeField] Transform xHairTrans;

    [SerializeField] Transform indicatorAnchor;
    [SerializeField] Transform indicatorFG;
    float indiciatorFGFullScaleX;
    float indicatorFGPosZFull, indicatorFGPosZZero;

    private void Start()
    {
        currentAimDir = currentInput = Vector2.zero;
        InitIndicator();
    }

    private void Update()
    {

        //var dev = refs_global.Instance.ip.I.Play.AimStick.ReadValue<Vector2>();
        //print(dev);

        // check for teleport
        if (refs_global.Instance.currentBallRefs != null && !refs_global.Instance.ballInHand &&
            refs_global.Instance.ip.I.Play.Teleport.WasPressedThisFrame())
        {
            HandleTeleport();
            return;
        }

        //currentInput = new Vector2(Input.GetAxisRaw("Aim Stick X"), Input.GetAxisRaw("Aim Stick Y"));
        currentInput = refs_global.Instance.ip.I.Play.AimStick.ReadValue<Vector2>().normalized;

        if (currentlyCharging)
        {
            //if (currentInput.sqrMagnitude < 0.0005f)
            if (currentInput == Vector2.zero)
            {
                ToggleIndicatorVisibility(false);
                InitThrow();
                return;
            }

            currentAimDir = currentInput;
            MoveCrosshair(currentInput);
            RotateIndicator();
        }
        else
        {
            if (!refs_global.Instance.ballInHand) return;
            //if (currentInput.sqrMagnitude > 0.00051f)
            if (currentInput != Vector2.zero)
            {
                currentlyCharging = true;
                currentCharge = refs.settings.throwForceBase;
                ToggleIndicatorVisibility(true);
            }
        }
    }

    private void MoveCrosshair(Vector2 dir)
    {
        Vector2 newPosClamped;
        newPosClamped = (Vector2)xHairTrans.localPosition + (dir.normalized * 10);
        newPosClamped = Vector2.ClampMagnitude(newPosClamped, refs.settings.xHairClamp);

        xHairTrans.localPosition = newPosClamped;
    }

    //private Vector2 ProcessInput(float x, float y, float deadThreshold)
    //{
    //    if (Mathf.Abs(x) <= deadThreshold) x = 0;
    //    if (Mathf.Abs(y) <= deadThreshold) y = 0;

    //    Vector2 finalInput = new Vector2(x, y);
    //    finalInput.Normalize();

    //    return finalInput;
    //}

    protected override void FixedUpdate()
    {
        if (!currentlyCharging) return;

        currentCharge += refs.settings.throwForceAdd * Time.fixedDeltaTime;

        if (currentCharge >= refs.settings.throwForceMax)
        {
            currentCharge = refs.settings.throwForceMax;
            ToggleIndicatorVisibility(false);
            InitThrow();
        }

        ScaleIndicatorCharge();
    }

    // ---------------------------------------------------------------------------------------
    // ------------------------------ VISUAL INDICATOR STUFF ---------------------------------
    // ---------------------------------------------------------------------------------------

    private void RotateIndicator()
    {
        Vector2 dir = currentAimDir.normalized;

        indicatorAnchor.LookAt(refs.bodyTrans.position + (Vector3)dir * 5);
        if (Mathf.Abs(dir.y) == 1)
        {
            indicatorAnchor.localRotation = Quaternion.Euler(new Vector3(indicatorAnchor.localEulerAngles.x, 90, 0));
        }
    }

    private void ScaleIndicatorCharge()
    {
        float currentLerpFactor = currentCharge / refs.settings.throwForceMax;

        float newScaleX = Mathf.Lerp(
            0,
            indiciatorFGFullScaleX,
            currentLerpFactor
            );

        indicatorFG.localScale = new Vector3(newScaleX, indicatorFG.localScale.y, indicatorFG.localScale.z);

        float newPosZ = Mathf.Lerp(
            indicatorFGPosZZero,
            indicatorFGPosZFull,
            currentLerpFactor
            );

        indicatorFG.localPosition = new Vector3(indicatorFG.localPosition.x, indicatorFG.localPosition.y, newPosZ);

    }

    private void InitIndicator()
    {
        indiciatorFGFullScaleX = indicatorFG.localScale.x;
        indicatorFGPosZFull = indicatorFG.localPosition.z;

        ResetIndicator();

        ToggleIndicatorVisibility(false);
    }

    private void ResetIndicator()
    {
        indicatorFG.localScale = new Vector3(0, indicatorFG.localScale.y, indicatorFG.localScale.z);

        indicatorFGPosZZero = indicatorFGPosZFull - indiciatorFGFullScaleX / 2;

        indicatorFG.localPosition = new Vector3(
            indicatorFG.localPosition.x,
            indicatorFG.localPosition.y,
            indicatorFGPosZZero
            );
    }

    private void ToggleIndicatorVisibility(bool state)
    {
        indicatorAnchor.gameObject.SetActive(state);
        xHairTrans.gameObject.SetActive(state);
    }
}
