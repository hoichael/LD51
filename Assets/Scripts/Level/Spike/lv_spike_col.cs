using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv_spike_col : MonoBehaviour
{
    [SerializeField] lv_spike_anim spikeAnim;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //spikeAnim.enabled = true;
    //    spikeAnim.Init();
    //}

    // called from pl_spikecheck and ball_spikecheck.
    // suboptimal but fine for now, quick and dirty way of limiting spike feedback to single spike (handled in spikechecks)
    public void HandleCol()
    {
        spikeAnim.Init();
    }
}
