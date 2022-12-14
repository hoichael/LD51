using System.Collections;
using UnityEngine;

public class pl_walljump : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    //[SerializeField] pl_gravity grav;
    int currentSide;
    Vector2 currentAddForce;

    private void Update()
    {
        if (!refs.info.grounded && !refs.info.jumpUsedThisFrame)
        {
            //if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
            if (refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame())
            {
                int wallCheckInt = CheckForWall();
                if (wallCheckInt != 0)
                {
                    refs.events.OnWallJump();

                    currentSide = wallCheckInt;
                    refs.info.dir = wallCheckInt;

                    ApplyForceBase(wallCheckInt);
                    StartCoroutine(HandleWalljumpFlag());
                    currentAddForce = refs.settings.walljump.forceAdd;
                }
            }
        }
    }

    public void Cancel()
    {
        currentAddForce = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if(currentAddForce != Vector2.zero)
        {
            ApplyForceAdd();
        }
    }

    // return int instead of bool because in case of wall, side of wall also needs to be known
    private int CheckForWall()
    {
        Vector2 wallCheckSize; // variable wall check size dependent on player X vel

        // determine wall check size for right wall and conduct check for right wall
        wallCheckSize = refs.rb.velocity.x > refs.settings.checks.wallCheckMoveThreshold 
            ? refs.settings.checks.wallCheckSizeMove : refs.settings.checks.wallCheckSizeStatic;

        if (Physics2D.OverlapBox(refs.bodyTrans.position + refs.settings.checks.wallCheckOffset, wallCheckSize, 0, refs.settings.checks.solidLayer) != null)
        {
            return -1;
        }

        // determine wall check size for left wall and conduct check for left wall
        wallCheckSize = refs.rb.velocity.x < -refs.settings.checks.wallCheckMoveThreshold 
            ? refs.settings.checks.wallCheckSizeMove : refs.settings.checks.wallCheckSizeStatic;

        if (Physics2D.OverlapBox(refs.bodyTrans.position - refs.settings.checks.wallCheckOffset, wallCheckSize, 0, refs.settings.checks.solidLayer) != null)
        {
            return 1;
        }

        return 0;
    }

    private void ApplyForceBase(int dir)
    {
        //refs.rb.velocity = new Vector2(refs.rb.velocity.x, 0);
        refs.rb.velocity = Vector2.zero;

        refs.rb.AddForce(
            new Vector2(refs.settings.walljump.dir.x * dir, refs.settings.walljump.dir.y).normalized
            * refs.settings.walljump.forceBase,
            ForceMode2D.Impulse);
    }

    private void ApplyForceAdd()
    {
        currentAddForce = Vector2.MoveTowards(currentAddForce, Vector2.zero, refs.settings.walljump.addForceResetSpeed * Time.deltaTime);

        refs.rb.AddForce(
            new Vector2(refs.settings.walljump.dir.x * currentSide, refs.settings.walljump.dir.y).normalized
            * currentAddForce,
            ForceMode2D.Force);
    }

    private IEnumerator HandleWalljumpFlag()
    {
        refs.info.recentWalljump = true;
        yield return new WaitForSeconds(0.25f);
        refs.info.recentWalljump = false;
    }
}

