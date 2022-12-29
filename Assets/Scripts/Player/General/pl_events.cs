using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_events : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_move_modify moveMod;
    [SerializeField] pl_fpadforce fPadHandler;
    [SerializeField] pl_spritedeform sprDeform;
    [SerializeField] pl_jump_buffer jumpBufferHandler;
    [SerializeField] pl_walljump wallJump;
    [SerializeField] pl_throw_manager throwManager;

    public void OnEnterGround()
    {
        refs.gravity.enabled = false;
        refs.info.grounded = true;
        refs.rb.drag = refs.settings.move.dragGround;
        refs.info.moveForceCurrent = refs.settings.move.forceGround;

        fPadHandler.Cancel();
        wallJump.Cancel();
        jumpBufferHandler.HandleEnterGround();
    }

    // important to note: this is NOT called upon teleport to mid-air ball (bc grounded is set false in OnTeleport and groundcheck uses grounded flag to determine frame of entry/exit ground)
    public void OnExitGround()
    {
        moveMod.HandleExitGround();

        refs.gravity.enabled = true;
        refs.info.grounded = false;
        refs.rb.drag = refs.settings.move.dragAir;
        refs.info.moveForceCurrent = refs.settings.move.forceAir;

        jumpBufferHandler.HandleExitGround();
    }

    public void OnWallJump()
    {
        refs.rb.drag = refs.settings.walljump.drag;
        refs.info.moveForceCurrent = refs.settings.move.forceAir;
        refs.gravity.gravCurrent = refs.settings.walljump.grav;
        refs.info.moveForceCurrent = 20f;
        //refs.info.moveForceCurrent = 1000;

        moveMod.HandleWalljump();
        fPadHandler.Cancel();
    }

    public void OnTeleport()
    {
        refs.gravity.enabled = true;
        refs.info.grounded = false;
        refs.rb.drag = 0.1f;
        refs.info.moveForceCurrent = 0.1f;

        moveMod.HandleTeleport(refs_global.Instance.currentBallRefs.rb.velocity);
        wallJump.Cancel();
        fPadHandler.Cancel();
    }

    public void OnForcepad()
    {
        refs.rb.drag = 0.1f;
        refs.info.moveForceCurrent = 0.1f;

        moveMod.HandleForcepad();
        fPadHandler.Init();
        sprDeform.OnForcepadTrigger();
        wallJump.Cancel();
    }

    public void OnDeath()
    {
        throwManager.Cancel();
        refs_global.Instance.levelManager.InitLevel(refs_global.Instance.levelManager.currentLevelIDX);
    }
}
