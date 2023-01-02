using UnityEngine;

public class pl_bodydeform : MonoBehaviour
{
    [SerializeField] bool deformOnJump;
    [SerializeField] bool deformOnLand;

    [SerializeField] pl_refs refs;
    [SerializeField] Transform modelTrans;

    Vector2 jumpDeformStartScale, landDeformTarget;

    bool currentlyResettingScale, currentlyInLandDeform;

    float currentFactor;

    private void FixedUpdate()
    {
        if (currentlyResettingScale)
        {
            HandleScaleReset();
        }

        if(currentlyInLandDeform)
        {
            HandleLandDeform();
        }
    }

    // called in FixedUpdate of jump_manager
    public void HandleJumpGrowth(float currentJumpTimer)
    {
        if (!deformOnJump) return;

        currentFactor = 0.18f + (currentJumpTimer / refs.settings.jump.addDuration);

        modelTrans.localScale = Vector3.Lerp(
            jumpDeformStartScale,
            refs.settings.visual.maxDeformJump,
            currentFactor
            );
    }

    public void OnForcepadTrigger()
    {
        modelTrans.localScale = refs.settings.visual.forcePadDeform;
        currentlyResettingScale /*= currentlyResettingRot*/ = true;
    }

    public void OnJumpTrigger()
    {
        if (!deformOnJump) return;

        jumpDeformStartScale = modelTrans.localScale;

        currentlyInLandDeform = false;
        currentFactor = 0;
    }

    private void HandleLandDeform()
    {
        currentFactor = Mathf.MoveTowards(currentFactor, 1, refs.settings.visual.landDeformSpeed);

        modelTrans.localScale = Vector3.Lerp(
            Vector3.one,
            landDeformTarget,
            Mathf.PingPong(currentFactor, 0.5f)
            );

        modelTrans.localPosition = new Vector3(0, -((1 - modelTrans.localScale.y)), 0);

        if (currentFactor == 1) currentlyInLandDeform = false;
    }

    private void HandleScaleReset()
    {
        modelTrans.localScale = Vector3.MoveTowards(modelTrans.localScale, Vector3.one, refs.settings.visual.resetSpeed);
        if (modelTrans.localScale == Vector3.one) currentlyResettingScale = false;
    }

    public void OnJumpTerminate()
    {
        if (!deformOnJump) return;

        currentFactor = 0;
        currentlyResettingScale = true;
    }

    public void OnLand(float velY) // called from pl_groundcheck
    {
        if (!deformOnLand) return;

        if (currentlyResettingScale) return;
        if (Mathf.Abs(velY) < refs.settings.visual.minVelYToDeformOnLand) return;

        currentFactor = 0;
        currentlyInLandDeform = true;

        float factor = Mathf.Clamp(Mathf.Abs(velY) - 10, 5, 75) / 75;

        landDeformTarget = Vector3.Lerp(
            Vector3.one,
            refs.settings.visual.maxDeformLand,
            factor
            );
    }
}
