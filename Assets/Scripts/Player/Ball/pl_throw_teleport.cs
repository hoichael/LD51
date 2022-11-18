using UnityEngine;

public class pl_throw_teleport : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] lv_pool pool;

    void Update()
    {
        if (refs_global.Instance.currentBallRefs != null && !refs_global.Instance.ballInHand &&
            refs_global.Instance.ip.I.Play.Teleport.WasPressedThisFrame())
        {
            HandleTeleport();
        }
    }

    private void HandleTeleport()
    {
        // set player pos and vel to ball
        refs.bodyTrans.position = refs_global.Instance.currentBallRefs.trans.position;
        refs.rb.velocity = refs_global.Instance.currentBallRefs.rb.velocity * 1.15f;

        refs.events.OnTeleport();

        // dispose of ball
        //Destroy(refs_global.Instance.currentBallRefs.trans.gameObject);
        refs_global.Instance.currentBallRefs.rb.gravityScale = 0;
        pool.Return(lv_pool.PoolType.Ball, refs_global.Instance.currentBallRefs.trans, false);
        refs_global.Instance.currentBallRefs = null;
    }
}
