using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_move_modify : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_gravity grav;

    float modFactorGrav, modFactorDrag, modFactorMove;

    public void HandleTeleport(Vector2 ballVel)
    {
        float mag = new Vector2(ballVel.x, ballVel.y * 0.16f).magnitude;
        modFactorDrag = Mathf.Clamp(60 - mag, 0.1f, 60);
        modFactorDrag = (modFactorDrag / 60) * 4f;

        modFactorMove = modFactorDrag * 40;

        if (Mathf.Abs(ballVel.x) < 1)
        {
            refs.info.moveForceCurrent = refs.settings.moveForceAir;
            modFactorMove = 0;
        }

        grav.gravCurrent = Mathf.Clamp((((60 - ballVel.magnitude) / 60) * refs.settings.gravBase) * 2, 70, 125);
    }

    public void HandleWalljump()
    {
        grav.gravCurrent = refs.settings.gravBaseWallJump;
        modFactorGrav = refs.settings.gravAdd * 1.7f;
        modFactorDrag = 2.4f;
    }

    public void HandleExitGround()
    {
        grav.gravCurrent = refs.settings.gravBase;
        modFactorGrav = refs.settings.gravAdd;
    }

    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void FixedUpdate()
    {
        if (refs.info.grounded) return;
        ModifyGravity();
        ModifyDrag();
        ModifyMovespeed();
    }

    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void ModifyGravity()
    {
        //grav.gravCurrent = Mathf.Clamp(grav.gravCurrent + refs.settings.gravAdd * Time.deltaTime, 0, refs.settings.gravMax);
        grav.gravCurrent = Mathf.MoveTowards(grav.gravCurrent, refs.settings.gravMax, modFactorGrav * Time.deltaTime);
    }

    private void ModifyDrag()
    {
        refs.rb.drag = Mathf.MoveTowards(refs.rb.drag, refs.settings.dragAir, modFactorDrag * Time.deltaTime);
    }

    private void ModifyMovespeed()
    {
        if (modFactorMove == 0) return;
        refs.info.moveForceCurrent = Mathf.MoveTowards(refs.info.moveForceCurrent, refs.settings.moveForceAir, modFactorMove * Time.deltaTime);
    }
}
