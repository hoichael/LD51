using UnityEngine;

public class ball_spikecheck : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Spike"))
        {
            refs.rb.gravityScale = 0;
            refs_global.Instance.currentBallRefs = null;
            refs_global.Instance.pool.Return(lv_pool.PoolType.Ball, refs.trans, false);
        }
    }
}
