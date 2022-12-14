using UnityEngine;

public class pl_jump_NEW : MonoBehaviour
{
    [SerializeField] bool enableSuperjump;
    [SerializeField] pl_refs refs;
    [SerializeField] Transform topcheckTrans;

    [SerializeField] float superJumpForceBase;
    [SerializeField] Vector2 superJumpCheckSize;
    [SerializeField] int maxSuperJumpCount;

    // public for DB
    public int superJumpCounter;


    // public for debug
    public bool jumpActive;
    // public for debug
    public float currentJumpTimer;

    private void Update()
    {
        if (refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame())
        {
            if (refs.info.grounded)
            {
                superJumpCounter = 0;
                InitJump();
            }
            else if (enableSuperjump && refs.info.slope == 0 && Physics2D.OverlapBox(refs.groundcheckTrans.position, superJumpCheckSize, 0, refs.settings.checks.solidLayer) != null)
            {
                print("SUPERJUMP");
                superJumpCounter = Mathf.Clamp(superJumpCounter + 1, 0, maxSuperJumpCount);
                InitJump();
            }
        }
        else if (jumpActive && refs_global.Instance.ip.I.Play.Jump.WasReleasedThisFrame())
        {
            TerminateJump();
        }
    }

    private void FixedUpdate()
    {
        if (!jumpActive) return;

        if ((currentJumpTimer += Time.fixedDeltaTime) > refs.settings.jump.addDuration)
        {
            TerminateJump();
        }
        else
        {
            ApplyForceAdd();
            HandleTopcheck();
        }
    }

    private void InitJump()
    {
        //refs.info.grounded = false; // hmmmmmm
        refs.rb.drag = refs.settings.move.dragGround;
        ApplyForceBase();
        jumpActive = true;
        currentJumpTimer = 0;
    }

    private void TerminateJump()
    {
        jumpActive = false;
        currentJumpTimer = 0;
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, refs.rb.velocity.y * refs.settings.jump.termMult);
    }

    private void ApplyForceBase()
    {
        // reset y vel before applying jump force for consistent jump
        //refs.rb.velocity = new Vector2(refs.rb.velocity.x, 0);
        if(refs.rb.velocity.y < 0)
        {
            refs.rb.velocity = new Vector2(refs.rb.velocity.x, 0);
        }

        // handle main force
        refs.rb.AddForce(new Vector2(
            refs.rb.velocity.x * 0.044f,
            refs.settings.jump.forceBase
            ),
            ForceMode2D.Impulse);

        // handle superjump force
        refs.rb.AddForce(new Vector2(
            (refs.rb.velocity.x * 0.05f) * ((superJumpForceBase * 0.4f) * superJumpCounter),
            superJumpForceBase * superJumpCounter
            ),
            ForceMode2D.Impulse);

    }

    private void HandleTopcheck()
    {
        if (Physics2D.OverlapBox(topcheckTrans.position, refs.settings.checks.groundCheckSize, 0, refs.settings.checks.solidLayer) != null)
        {
            TerminateJump();
        }
    }

    private void ApplyForceAdd()
    {
        refs.rb.AddForce(new Vector2(
            refs.rb.velocity.x * 0.012f,
            refs.settings.jump.forceAdd
            ),
            ForceMode2D.Impulse);
    }
}
