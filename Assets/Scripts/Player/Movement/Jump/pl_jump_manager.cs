using System.Collections;
using UnityEngine;

public class pl_jump_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform topcheckTrans;

    [SerializeField] pl_jump_flat jumpFlat;
    [SerializeField] pl_jump_slope jumpSlope;
    [SerializeField] pl_jump_buffer jumpBuffer;

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

        //if (refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame() && (refs.info.grounded || ExtendedGroundcheck()))
        if (refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame() && (refs.info.grounded || jumpBuffer.exitGroundBufferActive))
        {
            InitJump();
        }
    }

    private void FixedUpdate()
    {
        if (!jumpActive) return;

        if ((currentJumpTimer += Time.fixedDeltaTime) > refs.settings.jump.addDuration)
        {
            TerminateJump();
            return;
        }
        else if (jumpFlat.enabled)
        {
            HandleTopcheck();
        }

        if(jumpFlat.enabled)
        {
            sprDeform.HandleJumpGrowth(currentJumpTimer);
        }
    }

    public void InitJump()
    {
        if (refs.info.jumpUsedThisFrame || jumpActive) return;
        jumpActive = true;
        StartCoroutine(HandleJumpUsedThisFrame());

        refs.rb.drag = refs.settings.move.dragGround;
        refs.info.moveForceCurrent = refs.settings.move.forceGround;

        currentJumpTimer = 0;

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

    private void TerminateJump()
    {
        jumpActive = jumpFlat.enabled = jumpSlope.enabled = false;
        currentJumpTimer = 0;
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, refs.rb.velocity.y * refs.settings.jump.termMult);

        sprDeform.OnJumpTerminate();
    }

    private void HandleTopcheck()
    {
        if (Physics2D.OverlapBox(topcheckTrans.position, refs.settings.checks.groundCheckSize, 0, refs.settings.checks.solidLayer) != null)
        {
            TerminateJump();
        }
    }

    private IEnumerator HandleJumpUsedThisFrame()
    {
        refs.info.jumpUsedThisFrame = true;
        yield return new WaitForSeconds(0);
        refs.info.jumpUsedThisFrame = false;
    }

    //private bool ExtendedGroundcheck()
    //{
    //    if (Physics2D.OverlapBox(refs.groundcheckTrans.position, refs.settings.groundcheckSize + new Vector2(0, 0.9f), 0, refs.settings.solidLayer) != null)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}
