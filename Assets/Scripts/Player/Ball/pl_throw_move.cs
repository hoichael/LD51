using UnityEngine;

public class pl_throw_move : pl_throw_base
{
    void Update()
    {
        if (currentlyCharging)
        {
            // set aim dir to current movement input
            currentAimDir = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
                ).normalized;

            // check for throw
            if (Input.GetKeyUp(KeyCode.Mouse0) ||
                Input.GetKeyUp(KeyCode.K))
            {
                if(currentAimDir == Vector2.zero)
                {
                    currentAimDir = new Vector2(refs.info.dir, 0);
                }
                InitThrow();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K))
        {
            // check for init charge
            if (refs_global.Instance.ballInHand)
            {
                currentlyCharging = true;
                currentCharge = refs.settings.throwForceBase;
            }
            // check for teleport
            else if(refs_global.Instance.currentBallRefs != null)
            {
                HandleTeleport();
            }
        }
    }
}
