using UnityEngine;
using System.Collections;

public class ball_spikecheck : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    bool hasTouchedSpike;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (hasTouchedSpike) return;

        if (col.CompareTag("Spike"))
        {
            hasTouchedSpike = true;

            lv_spike_col spikeColHandler;
            col.gameObject.TryGetComponent<lv_spike_col>(out spikeColHandler);
            spikeColHandler?.HandleCol();

            if (ReferenceEquals(refs_global.Instance.currentBallRefs, refs)) {
                refs_global.Instance.currentBallRefs = null;
            }
            refs.rb.gravityScale = 0;
            refs_global.Instance.pool.Return(lv_pool.PoolType.Ball, refs.trans, false);
        }
    }

    private void OnEnable()
    {
        hasTouchedSpike = false;
    }
}
