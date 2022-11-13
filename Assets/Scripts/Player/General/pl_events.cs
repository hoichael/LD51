using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_events : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_move_modify moveMod;
    [SerializeField] pl_fpadforce fPadHandler;
    [SerializeField] pl_spritedeform sprDeform;

    public void OnEnterGround()
    {
        refs.gravity.enabled = false;
        refs.info.grounded = true;
        refs.rb.drag = refs.settings.dragGround;
        refs.info.moveForceCurrent = refs.settings.moveForceGround;

        fPadHandler.Cancel();
    }

    public void OnExitGround()
    {
        moveMod.HandleExitGround();

        refs.gravity.enabled = true;
        refs.info.grounded = false;
        refs.rb.drag = refs.settings.dragAir;
        refs.info.moveForceCurrent = refs.settings.moveForceAir;
    }

    public void OnWallJump()
    {
        refs.rb.drag = refs.settings.dragWalljump;
        refs.info.moveForceCurrent = refs.settings.moveForceAir;

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
        fPadHandler.Cancel();
    }

    public void OnForcepad()
    {
        refs.rb.drag = 0.1f;
        refs.info.moveForceCurrent = 0.1f;

        moveMod.HandleForcepad();
        fPadHandler.Init();
        sprDeform.OnForcepadTrigger();
    }
}
