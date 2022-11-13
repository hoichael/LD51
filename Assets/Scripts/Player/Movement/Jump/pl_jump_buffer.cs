using System.Collections;
using UnityEngine;

public class pl_jump_buffer : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_jump_manager jumpManager;
    [SerializeField] float enterGroundBufferDuration, exitGroundBufferDuration;
    bool enterGroundBufferActive;
    public bool exitGroundBufferActive;

    private void Update()
    {
        if (refs.info.grounded || enterGroundBufferActive) return;

        if (refs_global.Instance.ip.I.Play.Jump.WasPressedThisFrame())
        {
            StartCoroutine(HandleEnterBuffer());
        }
    }

    public void HandleEnterGround()
    {
        if (enterGroundBufferActive)
        {
            enterGroundBufferActive = false;
            jumpManager.InitJump();
        }
    }

    public void HandleExitGround()
    {
        StartCoroutine(HandleExitBuffer());
    }

    private IEnumerator HandleEnterBuffer()
    {
        enterGroundBufferActive = true;
        yield return new WaitForSeconds(enterGroundBufferDuration);
        enterGroundBufferActive = false;
    }

    private IEnumerator HandleExitBuffer()
    {
        exitGroundBufferActive = true;
        yield return new WaitForSeconds(exitGroundBufferDuration);
        exitGroundBufferActive = false;
    }

    private void OnDisable()
    {
        enterGroundBufferActive = exitGroundBufferActive = false;
        StopAllCoroutines();
    }
}
