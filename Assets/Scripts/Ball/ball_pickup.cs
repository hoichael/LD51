using UnityEngine;

public class ball_pickup : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (refs.manager.CurrentlyHoldingBall()) return;
        if (refs_global.Instance.ballInHand) return;
        refs_global.Instance.ballInHand = true;

        refs_global.Instance.currentBallRefs = refs;

        //refs.rb.isKinematic = true;
        //refs.rb.bodyType = RigidbodyType2D.Kinematic;
        Destroy(refs.rb);

        //refs_global.Instance.xHairTrans.gameObject.SetActive(true);

        refs.colSolid.enabled = false;
        refs.colTrigger.enabled = false;

        refs.trans.SetParent(refs_global.Instance.ballHolderTrans);
        refs.trans.localPosition = Vector3.zero;

        refs.trail.emitting = false;

        refs.manager.HandlePickup();
    }
}
