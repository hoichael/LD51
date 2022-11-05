using System.Collections;
using UnityEngine;

public class ball_manager : MonoBehaviour
{
    [SerializeField] ball_refs refs;

    public void HandlePickup()
    {
        if (refs_global.Instance.ballInHand) return;
        refs_global.Instance.ballInHand = true;

        refs_global.Instance.currentBallRefs = refs;

        Destroy(refs.rb);
        refs.rb = null;

        refs.colSolid.enabled = false;
        refs.colTrigger.enabled = false;

        refs.trans.SetParent(refs_global.Instance.ballHolderTrans);
        refs.trans.localPosition = Vector3.zero;

        refs.trail.emitting = false;
    }

    public void HandleThrow()
    {
        //refs.trans.SetParent(null);
        refs.colSolid.enabled = true;
        refs.trail.emitting = true;

        CreateRB();

        refs.trans.position = refs_global.Instance.playerTrans.position;

        StartCoroutine(DelayedColHandler());
    }

    private void CreateRB()
    {
        Rigidbody2D rb = refs.trans.gameObject.AddComponent<Rigidbody2D>();
        rb.sharedMaterial = refs.settings.rbPhMat;
        rb.gravityScale = refs.settings.rbGravityScale;
        rb.collisionDetectionMode = refs.settings.rbColDetectMode;
        rb.interpolation = refs.settings.rbInterp;

        refs.rb = rb;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        refs.colTrigger.enabled = true;
        if (refs.rb == null) CreateRB();
    }

    private IEnumerator DelayedColHandler()
    {
        yield return new WaitForSeconds(refs.settings.colEnableDelay);
        refs.colTrigger.enabled = true;
    }
}
