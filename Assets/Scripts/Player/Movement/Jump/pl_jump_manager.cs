using UnityEngine;

public class pl_jump_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform topcheckTrans;

    [SerializeField] pl_jump_flat jumpFlat;
    [SerializeField] pl_jump_slope jumpSlope;

    [SerializeField] pl_spritedeform sprDeform;

    bool jumpActive;
    float currentJumpTimer;

    private void Awake()
    {
        jumpFlat.enabled = jumpSlope.enabled = false;
    }

    private void Update()
    {
        if (jumpActive)
        {
            if (refs_global.Instance.ip.I.Play.Jump.WasReleasedThisFrame())
            {
                TerminateJump();
            }

            return;
        }

        if (refs.info.recentWalljump) return;
        
        if (refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame() && (refs.info.grounded || ExtendedGroundcheck()))
        {
            InitJump();

            if (refs.info.slope == 0)
            {
                jumpFlat.enabled = true;
                sprDeform.OnJumpTrigger();
            }
            else
            {
                jumpSlope.enabled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!jumpActive) return;

        if ((currentJumpTimer += Time.fixedDeltaTime) > refs.settings.jumpAddDuration)
        {
            TerminateJump();
            return;
        }
        else
        {
            HandleTopcheck();
        }

        sprDeform.HandleJumpGrowth(currentJumpTimer);
    }

    private void InitJump()
    {
        refs.rb.drag = refs.settings.dragGround;
        jumpActive = true;
        currentJumpTimer = 0;

        refs.info.jumpUsedThisFrame = true;
    }

    private void TerminateJump()
    {
        jumpActive = jumpFlat.enabled = jumpSlope.enabled = false;
        currentJumpTimer = 0;
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, refs.rb.velocity.y * refs.settings.jumpTermMult);

        refs.info.jumpUsedThisFrame = false; // technically doesnt belong here but it works, and this way I dont have to reset the flag every frame

        sprDeform.OnJumpTerminate();
    }

    private void HandleTopcheck()
    {
        if (Physics2D.OverlapBox(topcheckTrans.position, refs.settings.groundcheckSize, 0, refs.settings.solidLayer) != null)
        {
            TerminateJump();
        }
    }

    private bool ExtendedGroundcheck()
    {
        if (Physics2D.OverlapBox(refs.groundcheckTrans.position, refs.settings.groundcheckSize + new Vector2(0, 0.9f), 0, refs.settings.solidLayer) != null)
        {
            return true;
        }
        return false;
    }
}
