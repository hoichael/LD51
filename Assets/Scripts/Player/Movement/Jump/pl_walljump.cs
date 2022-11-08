using System.Collections;
using UnityEngine;

public class pl_walljump : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_gravity grav;

    private void Update()
    {
        if(!refs.info.grounded)
        {
            //if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
            if(refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame())
            {
                int wallCheckInt = CheckForWall();
                if (wallCheckInt != 0)
                {
                    grav.HandleWalljump(); // a lil too tightly coupled but whtv
                    refs.FlipContainerTrans.localScale = new Vector3(wallCheckInt, 1, 1);
                    ApplyForce(wallCheckInt);
                    StartCoroutine(HandleWalljumpFlag());
                }
            }
        }
    }

    // return int instead of bool because in case of wall, side of wall also needs to be known
    private int CheckForWall()
    {
        // check right side for wall
        if (Physics2D.OverlapBox(refs.bodyTrans.position + refs.settings.wallCheckOffset, refs.settings.wallCheckSize, 0, refs.settings.solidLayer) != null)
        {
            return -1;
        }

        // check left side for wall
        if (Physics2D.OverlapBox(refs.bodyTrans.position - refs.settings.wallCheckOffset, refs.settings.wallCheckSize, 0, refs.settings.solidLayer) != null)
        {
            return 1;
        }

        return 0;
    }

    private void ApplyForce(int dir)
    {
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, 0);

        refs.rb.AddForce(
            new Vector2(refs.settings.wallJumpDir.x * dir, refs.settings.wallJumpDir.y).normalized
            * refs.settings.wallJumpForce,
            ForceMode2D.Impulse);
    }

    private IEnumerator HandleWalljumpFlag()
    {
        refs.info.recentWalljump = true;
        yield return new WaitForSeconds(0.25f);
        refs.info.recentWalljump = false;
    }
}

