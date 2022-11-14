using UnityEngine;

public class pl_throw_indicator : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_throw_manager manager;
    [SerializeField] Transform xHairTrans;
    [SerializeField] Transform indicatorAnchor;
    [SerializeField] Transform indicatorFG;
    float indiciatorFGFullScaleX;
    float indicatorFGPosZFull, indicatorFGPosZZero;

    private void Start()
    {
        InitIndicator();
    }

    public void UpdateIndicator(Vector2 dir, float charge)
    {
        MoveCrosshair(dir);
        RotateIndicator(dir);
        ScaleIndicatorCharge(charge);
    }

    public void HandleThrow()
    {
        ResetIndicator();
        ToggleIndicatorVisibility(false);
    }

    #region UPDATE
    private void MoveCrosshair(Vector2 dir)
    {
        Vector2 newPosClamped;
        newPosClamped = (Vector2)xHairTrans.localPosition + (dir.normalized * 10);
        newPosClamped = Vector2.ClampMagnitude(newPosClamped, refs.settings.ballThrow.xHairOffset);

        xHairTrans.localPosition = newPosClamped;
    }

    private void RotateIndicator(Vector2 dir)
    {
        dir.Normalize();

        indicatorAnchor.LookAt(refs.bodyTrans.position + (Vector3)dir * 5);
        if (Mathf.Abs(dir.y) == 1)
        {
            indicatorAnchor.localRotation = Quaternion.Euler(new Vector3(indicatorAnchor.localEulerAngles.x, 90, 0));
        }
    }

    private void ScaleIndicatorCharge(float currentCharge)
    {
        float currentLerpFactor = currentCharge / refs.settings.ballThrow.forceMax;

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
    #endregion

    #region LIFECYCLE
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

    public void ToggleIndicatorVisibility(bool state)
    {
        indicatorAnchor.gameObject.SetActive(state);
        xHairTrans.gameObject.SetActive(state);
    }
    #endregion
}
