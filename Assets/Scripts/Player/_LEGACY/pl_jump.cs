using UnityEngine;

public class pl_jump : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform topcheckTrans;

    // public for debug
    public bool jumpActive;
    // public for debug
    public float currentJumpTimer;

    private void Update()
    {
        //if (refs.info.grounded && Input.GetKeyDown(KeyCode.Space)) InitJump();
        //if (Input.GetKeyUp(KeyCode.Space)) TerminateJump();

        //if(refs.info.grounded)
        //{
        //    if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        //    {
        //        InitJump();
        //    }
        //}
        //else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump"))
        //{
        //    TerminateJump();
        //}

        if (refs.info.grounded)
        {
            if (refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame())
            {
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

        if((currentJumpTimer += Time.fixedDeltaTime) > refs.settings.jump.addDuration)
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
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, 0);

        refs.rb.AddForce(new Vector2(
            refs.rb.velocity.x * 0.05f,
            refs.settings.jump.forceBase
            ),
            ForceMode2D.Impulse);
    }

    private void HandleTopcheck()
    {
        if (Physics2D.OverlapBox(topcheckTrans.position, refs.settings.groundcheckSize, 0, refs.settings.solidLayer) != null)
        {
            TerminateJump();
        }
    }

    private void ApplyForceAdd()
    {
        refs.rb.AddForce(new Vector2(
            refs.rb.velocity.x * 0.02f,
            refs.settings.jump.forceAdd
            ),
            ForceMode2D.Impulse);
    }
}
