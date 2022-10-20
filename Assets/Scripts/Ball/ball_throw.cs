using System.Collections;
using UnityEngine;

public class ball_throw : MonoBehaviour
{
    [SerializeField] ball_refs refs;
 
    /*
    public void Init(Vector2 dir , float force)
    {
        refs.trans.SetParent(null);
        refs.colSolid.enabled = true;
        refs.trail.emitting = true;

        refs_global.Instance.xHairTrans.gameObject.SetActive(false);

        //refs.rb.isKinematic = false;
        //refs.rb.bodyType = RigidbodyType2D.Dynamic;
        CreateRB();

        //ApplyForce(GetThrowDir(), force);
        ApplyForce(dir, force);

        StartCoroutine(DelayedColHandler());
    }

    private void ApplyForce(Vector2 dir, float force)
    {
        // apply main force based on player input
        refs.rb.AddForce(dir * force, ForceMode2D.Impulse);

        // apply secondary force based on player vel
        // refs.rb.AddForce(refs_global.Instance.playerRB.velocity * refs.settings.playerVelThrowForceMult, ForceMode2D.Impulse);

        // if dir doesnt point downwards apply slight tertiary upwards force
        if(dir.y >= 0)
        {
            refs.rb.AddForce(Vector2.up * (refs.settings.throwForceBase * 0.18f), ForceMode2D.Impulse);
        }
    }

    private Vector2 GetThrowDir()
    {
        //Vector3 pointerInWorldPos = refs_global.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
        //return (pointerInWorldPos - refs_global.Instance.playerTrans.position).normalized;
        return (refs_global.Instance.xHairTrans.position - refs_global.Instance.playerTrans.position).normalized;
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

    private IEnumerator DelayedColHandler()
    {
        yield return new WaitForSeconds(refs.settings.colEnableDelay);
        refs.colTrigger.enabled = true;
    }
    */
}
