using UnityEngine;

public class pl_bodydeform : MonoBehaviour
{
    [SerializeField] bool deformOnJump;
    [SerializeField] bool deformOnLand;

    [SerializeField] pl_refs refs;
    [SerializeField] Transform modelTrans;

    //[SerializeField] Vector3 maxDeformJump, maxDeformLand;
    //[SerializeField] float resetSpeed;
    //[SerializeField] float landDeformSpeed;

    //[SerializeField] Vector3 fPadDeform;

    Vector2 jumpDeformStartScale, landDeformTarget;

    Vector3 landRotTarget;

    bool currentlyResettingScale, currentlyResettingRot, currentlyInLandDeform;

    float currentFactor;

    private void FixedUpdate()
    {
        if (currentlyResettingScale)
        {
            HandleScaleReset();
        }

        if(currentlyResettingRot)
        {
            HandleRotReset();
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
        currentlyResettingScale = currentlyResettingRot = true;
    }

    public void OnJumpTrigger()
    {
        if (!deformOnJump) return;

        jumpDeformStartScale = modelTrans.localScale;

        modelTrans.localRotation = Quaternion.Euler(Vector3.zero);

        currentlyInLandDeform = false;
        currentlyResettingRot = true;
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

        //modelTrans.localPosition = new Vector3(0, -((1 - modelTrans.localScale.y)), refs.settings.visual.playerModelBasePosZ);
        modelTrans.localPosition = new Vector3(0, -((1 - modelTrans.localScale.y)), 0);

        modelTrans.localRotation = Quaternion.Lerp(
            Quaternion.Euler(Vector3.zero),
            Quaternion.Euler(landRotTarget),
            Mathf.PingPong(currentFactor, 0.5f)
            );

        //if (modelTrans.localScale == Vector3.one) currentlyInLandDeform = false;
        if (currentFactor == 1) currentlyInLandDeform = false;
    }

    private void HandleScaleReset()
    {
        modelTrans.localScale = Vector3.MoveTowards(modelTrans.localScale, Vector3.one, refs.settings.visual.resetSpeed);
        if (modelTrans.localScale == Vector3.one) currentlyResettingScale = false;
    }

    private void HandleRotReset()
    {
        modelTrans.localRotation = Quaternion.Euler(Vector3.MoveTowards(modelTrans.localRotation.eulerAngles, Vector3.zero, refs.settings.visual.resetSpeed));
        if (modelTrans.localRotation.eulerAngles == Vector3.zero) currentlyResettingRot = false;
    }

    public void OnJumpTerminate()
    {
        if (!deformOnJump) return;

        currentFactor = 0;
        currentlyResettingScale = true;
    }

    public void OnLand(float velY)
    {
        if (!deformOnLand) return;

        if (currentlyResettingScale) return;
        if (Mathf.Abs(velY) < 24) return;

        currentFactor = 0;
        currentlyInLandDeform = true;

        float factor = Mathf.Clamp(Mathf.Abs(velY) - 10, 5, 75) / 75;

        landDeformTarget = Vector3.Lerp(
            Vector3.one,
            refs.settings.visual.maxDeformLand,
            factor
            );

        float rotAdd = Mathf.Lerp(
            0,
            Mathf.Abs(refs.settings.visual.maxRotLandAddY),
            factor
            );

        //landRotTarget = Vector3.Lerp(
        //    new Vector3(0, -30, 0),
        //    refs.settings.visual.maxRotLand,
        //    factor
        //    );

        landRotTarget = new Vector3(0, refs.settings.visual.minRotLandY - rotAdd, 0);
    }
}
