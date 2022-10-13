using UnityEngine;

public class pl_jump : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform topcheckTrans;
    [SerializeField] LayerMask groundMask;

    // public for debug
    public bool jumpActive;
    // public for debug
    public float currentJumpTimer;

    private void Update()
    {
        if (refs.info.grounded && Input.GetKeyDown(KeyCode.Space)) InitJump();
        if (Input.GetKeyUp(KeyCode.Space)) TerminateJump();
    }

    private void FixedUpdate()
    {
        if (!jumpActive) return;

        if((currentJumpTimer += Time.fixedDeltaTime) > refs.settings.jumpAddDuration)
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
        refs.info.grounded = false; // hmmmmmm

        ApplyForceBase();
        jumpActive = true;
        currentJumpTimer = 0;
    }

    private void TerminateJump()
    {
        jumpActive = false;
        currentJumpTimer = 0;
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, refs.rb.velocity.y * refs.settings.jumpTermMult);
    }

    private void ApplyForceBase()
    {
        // reset y vel before applying jump force for consistent jump
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, 0);

        refs.rb.AddForce(new Vector2(
            refs.rb.velocity.x * 0.05f,
            refs.settings.jumpForceBase
            ),
            ForceMode2D.Impulse);
    }

    private void HandleTopcheck()
    {
        if (Physics2D.OverlapBox(topcheckTrans.position, refs.settings.groundcheckSize, 0, groundMask) != null)
        {
            TerminateJump();
        }
    }

    private void ApplyForceAdd()
    {
        refs.rb.AddForce(new Vector2(
            refs.rb.velocity.x * 0.02f,
            refs.settings.jumpForceAdd
            ),
            ForceMode2D.Impulse);
    }
}
