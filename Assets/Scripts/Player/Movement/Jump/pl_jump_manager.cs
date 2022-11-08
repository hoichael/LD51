using UnityEngine;

public class pl_jump_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform topcheckTrans;

    [SerializeField] pl_jump_flat jumpFlat;
    [SerializeField] pl_jump_slope jumpSlope;

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
        
        if (refs.info.grounded && refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame())
        {
            InitJump();

            if (refs.info.slope == 0)
            {
                jumpFlat.enabled = true;
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
        }
        else
        {
            HandleTopcheck();
        }
    }

    private void InitJump()
    {
        refs.rb.drag = refs.settings.dragGround;
        jumpActive = true;
        currentJumpTimer = 0;
    }

    private void TerminateJump()
    {
        jumpActive = jumpFlat.enabled = jumpSlope.enabled = false;
        currentJumpTimer = 0;
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, refs.rb.velocity.y * refs.settings.jumpTermMult);
    }

    private void HandleTopcheck()
    {
        if (Physics2D.OverlapBox(topcheckTrans.position, refs.settings.groundcheckSize, 0, refs.settings.solidLayer) != null)
        {
            TerminateJump();
        }
    }
}