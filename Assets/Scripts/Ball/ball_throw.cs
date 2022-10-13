using System.Collections;
using UnityEngine;

public class ball_throw : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    public void Init(Vector2 dir)
    {
        refs.trans.SetParent(null);
        refs.colSolid.enabled = true;
        refs.trail.emitting = true;

        //refs.rb.isKinematic = false;
        //refs.rb.bodyType = RigidbodyType2D.Dynamic;
        CreateRB();

        ApplyForce(dir);

        StartCoroutine(DelayedColHandler());
    }

    private void ApplyForce(Vector2 dir)
    {
        // apply main force into passed dir
        refs.rb.AddForce(dir * refs.settings.throwForceBase, ForceMode2D.Impulse);

        // if dir doesnt point downwards apply slight secondary upwards force
        if(dir.y >= 0)
        {
            refs.rb.AddForce(Vector2.up * (refs.settings.throwForceBase * 0.22f), ForceMode2D.Impulse);
        }
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


}
