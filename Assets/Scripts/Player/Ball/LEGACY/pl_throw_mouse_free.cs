/*
using UnityEngine;

public class pl_throw_mouse_free : pl_throw_base
{
    [SerializeField] Texture2D texCursor;

    private void Update()
    {
        if (currentlyCharging)
        {
            currentAimDir = GetAimDir();

            // check for throw
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                InitThrow();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // check for init charge
            if (refs_global.Instance.ballInHand)
            {
                currentlyCharging = true;
                currentCharge = refs.settings.throwForceBase;
            }
            // check for teleport
            else if (refs_global.Instance.currentBallRefs != null)
            {
                HandleTeleport();
            }
        }
    }

    private Vector2 GetAimDir()
    {
        Vector3 pointerInWorldPos = refs_global.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
        return (pointerInWorldPos - refs.bodyTrans.position).normalized;
    }

    private void OnEnable()
    {
        Cursor.SetCursor(texCursor, new Vector2(7.5f, 4.75f), CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
    }
}
*/