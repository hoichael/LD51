using UnityEngine;

public class pl_bodydeform : MonoBehaviour
{
    [SerializeField] bool deformOnJump;
    [SerializeField] bool deformOnLand;

    [SerializeField] pl_refs refs;
    [SerializeField] Transform sprTrans;

    //[SerializeField] Vector3 maxDeformJump, maxDeformLand;
    //[SerializeField] float resetSpeed;
    //[SerializeField] float landDeformSpeed;

    //[SerializeField] Vector3 fPadDeform;

    Vector2 jumpDeformStartScale, landDeformTarget;

    bool currentlyResetting, currentlyInLandDeform;

    float currentFactor;

    private void FixedUpdate()
    {
        if (currentlyResetting)
        {
            HandleReset();
        }
        else if(currentlyInLandDeform)
        {
            HandleLandDeform();
        }
    }

    // called in FixedUpdate of jump_manager
    public void HandleJumpGrowth(float currentJumpTimer)
    {
        if (!deformOnJump) return;

        currentFactor = 0.18f + (currentJumpTimer / refs.settings.jump.addDuration);

        sprTrans.localScale = Vector3.Lerp(
            jumpDeformStartScale,
            refs.settings.visual.maxDeformJump,
            currentFactor
            );
    }

    public void OnForcepadTrigger()
    {
        sprTrans.localScale = refs.settings.visual.forcePadDeform;
        currentlyResetting = true;
    }

    public void OnJumpTrigger()
    {
        if (!deformOnJump) return;

        jumpDeformStartScale = sprTrans.localScale;
        currentlyInLandDeform = false;
        currentFactor = 0;
    }

    private void HandleLandDeform()
    {
        currentFactor = Mathf.MoveTowards(currentFactor, 1, refs.settings.visual.landDeformSpeed);

        sprTrans.localScale = Vector3.Lerp(
            Vector3.one,
            landDeformTarget,
            Mathf.PingPong(currentFactor, 0.5f)
            );

        sprTrans.localPosition = new Vector3(0, -((1 - sprTrans.localScale.y)), 0);

        if (sprTrans.localScale == Vector3.one) currentlyInLandDeform = false;
    }

    private void HandleReset()
    {
        sprTrans.localScale = Vector3.MoveTowards(sprTrans.localScale, Vector3.one, refs.settings.visual.resetSpeed);
        if (sprTrans.localScale == Vector3.one) currentlyResetting = false;
    }

    public void OnJumpTerminate()
    {
        if (!deformOnJump) return;

        currentFactor = 0;
        currentlyResetting = true;
    }

    public void OnLand(float velY)
    {
        if (!deformOnLand) return;

        if (currentlyResetting) return;
        if (Mathf.Abs(velY) < 24) return;

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
