using UnityEngine;

public class pl_spritedeform : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform sprTrans;

    [SerializeField] Vector3 maxDeformJump, maxDeformLand;
    [SerializeField] float resetSpeed;

    bool currentlyResetting;

    float currentFactor;
    Vector2 jumpScaleApex;


    private void Update()
    {
        if (currentlyResetting)
        {
            HandleReset();
        }
    }

    // called in FixedUpdate of jump_manager
    public void HandleJumpGrowth(float currentJumpTimer)
    {
        currentFactor = 0.18f + (currentJumpTimer / refs.settings.jumpAddDuration);

        sprTrans.localScale = Vector3.Lerp(
            Vector3.one,
            maxDeformJump,
            currentFactor
            );
    }

    private void HandleReset()
    {
        sprTrans.localScale = Vector3.MoveTowards(sprTrans.localScale, Vector3.one, resetSpeed * Time.deltaTime);
        if (sprTrans.localScale == Vector3.one) currentlyResetting = false;
    }

    public void OnJumpTerminate()
    {
        jumpScaleApex = sprTrans.localScale;
        currentFactor = 0;
        currentlyResetting = true;
    }

    public void OnJumpTrigger()
    {

    }

    public void OnLand(float velY)
    {

    }
}
