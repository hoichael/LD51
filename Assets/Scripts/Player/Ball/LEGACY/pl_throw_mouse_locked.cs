/*
using UnityEngine;

public class pl_throw_mouse_locked : pl_throw_base
{
    [SerializeField] Transform xHairTrans;
    Vector2 currentMouseMoveInput, newPosClamped;
    void Update()
    {
        MoveCrosshair();

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

    private void MoveCrosshair()
    {
        currentMouseMoveInput = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
            );

        newPosClamped = (Vector2)xHairTrans.localPosition + currentMouseMoveInput;
        newPosClamped = Vector2.ClampMagnitude(newPosClamped, refs.settings.xHairClamp);

        xHairTrans.localPosition = newPosClamped;
    }

    private Vector2 GetAimDir()
    {
        return (xHairTrans.position - refs.bodyTrans.position).normalized;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        xHairTrans.gameObject.SetActive(true);
        xHairTrans.localPosition = new Vector2(refs.settings.xHairClamp * refs.info.dir, 0);
    }

    private void OnDisable()
    {
        xHairTrans.gameObject.transform.gameObject.transform.gameObject.transform.gameObject.transform.gameObject.transform.gameObject.transform.gameObject.transform.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }
}
*/