/*
using UnityEngine;

public class pl_throw_arrows : pl_throw_base
{
    void Update()
    {
        // check for teleport
        if (refs_global.Instance.currentBallRefs != null && !refs_global.Instance.ballInHand && Input.GetKeyDown(KeyCode.LeftShift))
        {
            HandleTeleport();
        }

        if (currentlyCharging)
        {
            // check for throw
            if (Input.GetKeyUp(KeyCode.UpArrow) ||
                Input.GetKeyUp(KeyCode.DownArrow) ||
                Input.GetKeyUp(KeyCode.LeftArrow) ||
                Input.GetKeyUp(KeyCode.RightArrow))
            {
                InitThrow();
            }

            // set new aim direction to current input after checking if input released
            currentAimDir = new Vector2(
                Input.GetAxisRaw("Aim Horizontal"),
                Input.GetAxisRaw("Aim Vertical")
                ).normalized;
        }
        else if (Input.GetAxisRaw("Aim Horizontal") != 0 || Input.GetAxisRaw("Aim Vertical") != 0)
        {
            if (refs_global.Instance.ballInHand)
            {
                currentlyCharging = true;
                currentCharge = refs.settings.throwForceBase;
            }
        }
    }
}
*/
