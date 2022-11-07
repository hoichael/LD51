using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_jump_NEW : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform topcheckTrans;
    [SerializeField] LayerMask groundMask;

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
            else if (Physics2D.OverlapBox(refs.groundcheckTrans.position, superJumpCheckSize, 0, groundMask) != null)
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

        if ((currentJumpTimer += Time.fixedDeltaTime) > refs.settings.jumpAddDuration)
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
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, refs.rb.velocity.y * refs.settings.jumpTermMult);
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
            refs.rb.velocity.x * 0.05f,
            refs.settings.jumpForceBase
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
