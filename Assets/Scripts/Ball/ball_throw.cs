using System.Collections;
using UnityEngine;

public class ball_throw : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    public void Init(Vector2 dir)
    {
        refs.trans.SetParent(null);
        refs.colSolid.enabled = true;
        //refs.rb.isKinematic = false;
        //refs.rb.bodyType = RigidbodyType2D.Dynamic;
        CreateRB();

        refs.rb.AddForce(dir * refs.settings.throwForceBase, ForceMode2D.Impulse);

        StartCoroutine(DelayedColHandler());
    }

    private void CreateRB()
    {
        Rigidbody2D rb = refs.trans.gameObject.AddComponent<Rigidbody2D>();
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
