/*
using UnityEngine;

public class pl_throw_arrows_visual : pl_throw_base
{
    [SerializeField] Transform indicatorAnchor;
    [SerializeField] Transform indicatorFG;
    float indiciatorFGFullScaleX;
    float indicatorFGPosZFull, indicatorFGPosZZero;

    private void Start()
    {
        InitIndicator();
    }

    void Update()
    {
        // check for teleport
        if (refs_global.Instance.currentBallRefs != null && !refs_global.Instance.ballInHand && Input.GetKeyDown(KeyCode.LeftShift))
        {
            HandleTeleport();
        }

        if (currentlyCharging)
        {
            // check for throw
            if (Input.GetKeyUp(KeyCode.UpArrow) ||
                Input.GetKeyUp(KeyCode.DownArrow) ||
                Input.GetKeyUp(KeyCode.LeftArrow) ||
                Input.GetKeyUp(KeyCode.RightArrow))
            {
                indicatorAnchor.gameObject.SetActive(false);
                InitThrow();
                return;
            }

            // set new aim direction to current input after checking if input released
            currentAimDir = new Vector2(
                Input.GetAxisRaw("Aim Horizontal"),
                Input.GetAxisRaw("Aim Vertical")
                ).normalized;

            RotateIndicator();
        }
        else if (Input.GetAxisRaw("Aim Horizontal") != 0 || Input.GetAxisRaw("Aim Vertical") != 0)
        {
            if (refs_global.Instance.ballInHand)
            {
                currentlyCharging = true;
                currentCharge = refs.settings.throwForceBase;
                indicatorAnchor.gameObject.SetActive(true);
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (!currentlyCharging) return;

        currentCharge += refs.settings.throwForceAdd * Time.fixedDeltaTime;

        if (currentCharge >= refs.settings.throwForceMax)
        {
            currentCharge = refs.settings.throwForceMax;
            indicatorAnchor.gameObject.SetActive(false);
            InitThrow();
        }

        ScaleIndicatorCharge();
    }

    private void RotateIndicator()
    {
        indicatorAnchor.LookAt(refs.bodyTrans.position + (Vector3)currentAimDir * 5);
        if(Mathf.Abs(currentAimDir.y) == 1)
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

        indicatorAnchor.gameObject.SetActive(false);
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
}
*/