using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_move_modify : MonoBehaviour
{
    [SerializeField] pl_refs refs;

    float modFactorGrav, modFactorDrag, modFactorMove;

    // this absolute abomination of a function needs a serious refactor. god help me
    public void HandleTeleport(Vector2 ballVel)
    {
        float mag = new Vector2(ballVel.x, ballVel.y * 0.16f).magnitude;
        modFactorDrag = Mathf.Clamp(60 - mag, 0.1f, 60);
        modFactorDrag = (modFactorDrag / 60) * 4f;

        modFactorMove = modFactorDrag * 40;
        //modFactorMove *= 50;

        if (Mathf.Abs(ballVel.x) < 1)
        {
            refs.info.moveForceCurrent = refs.settings.move.forceAir;
            modFactorMove = 0;
        }

        float ballMagFactor = Mathf.Clamp((60 - ballVel.magnitude) / 60, 0, 1);

        //refs.gravity.gravCurrent = Mathf.Clamp((((60 - ballVel.magnitude) / 60) * refs.settings.gravity.forceBase) * 2, 3500, 6250);
        refs.gravity.gravCurrent = Mathf.Clamp((ballMagFactor * refs.settings.gravity.forceBase) * 2, 3500, refs.settings.gravity.forceBase);

        //refs.gravity.gravCurrent *= 50;
        //print(refs.gravity.gravCurrent);


        modFactorGrav = refs.settings.gravity.forceAdd;
    }

    public void HandleWalljump()
    {
        //grav.gravCurrent = refs.settings.gravBaseWallJump;
        //modFactorGrav = refs.settings.gravAdd * 1.7f;
        modFactorDrag = 2f;
        modFactorMove = 94f;
        //modFactorMove = 4700;
    }

    public void HandleExitGround()
    {
        refs.gravity.gravCurrent = refs.settings.gravity.forceBase;
        modFactorGrav = refs.settings.gravity.forceAdd;
    }

    public void HandleForcepad()
    {
        modFactorMove = 94f;
        //modFactorMove = 4700;
        modFactorDrag = 0.9f;
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
        refs.gravity.gravCurrent = Mathf.MoveTowards(refs.gravity.gravCurrent, refs.settings.gravity.forceMax, modFactorGrav * Time.deltaTime);
    }

    private void ModifyDrag()
    {
        refs.rb.drag = Mathf.MoveTowards(refs.rb.drag, refs.settings.move.dragAir, modFactorDrag * Time.deltaTime);
    }

    private void ModifyMovespeed()
    {
        if (modFactorMove == 0) return;
        refs.info.moveForceCurrent = Mathf.MoveTowards(refs.info.moveForceCurrent, refs.settings.move.forceAir, modFactorMove * Time.deltaTime);
    }
}
