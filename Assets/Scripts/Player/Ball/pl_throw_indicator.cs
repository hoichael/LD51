using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_throw_indicator : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] List<SpriteRenderer> spotSpriteList;
    [SerializeField] Transform spotsAnchor;
    int currentStep;

    Vector2 currentDir; // for performance

    private void Start()
    {
        ResetIndicator();
        SetVisibility(false);
    }

    public void InitCharge()
    {
        ResetIndicator();
        HandleNewStep();
        SetVisibility(true);
    }

    public void UpdateRotation(Vector2 dir)
    {
        if (dir == currentDir) return; // for performance
        currentDir = dir;

        dir.Normalize();

        //float angle = 360 - (Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg * Mathf.Sign(dir.x));

        float angleSignMult = dir.y >= 0 ? 1 : -1;
        float angleOffsetAdd = angleSignMult >= 0 ? 0 : 360;
        float angle = Vector2.Angle(Vector2.right, dir) * angleSignMult + angleOffsetAdd;

        spotsAnchor.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void HandleNewStep()
    {
        currentStep++;
        SetSpotColor(currentStep, refs.settings.ballThrow.spotOpacityActive);
    }

    private void ResetIndicator()
    {
        currentStep = -1;
        for (int i = 0; i < 4; i++)
        {
            SetSpotColor(i, refs.settings.ballThrow.spotOpacityDefault);
        }
    }

    private void SetSpotColor(int idx, float opacity)
    {
        spotSpriteList[idx].color = new Color(
            refs.settings.ballThrow.SpotPaletteA[idx].r,
            refs.settings.ballThrow.SpotPaletteA[idx].g,
            refs.settings.ballThrow.SpotPaletteA[idx].b,
            opacity);
    }

    public void HandleThrow()
    {
        ResetIndicator();
        SetVisibility(false);
    }

    private void SetVisibility(bool state)
    {
        spotsAnchor.gameObject.SetActive(state);
    }
}
