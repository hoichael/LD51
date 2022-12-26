using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv_spike_col : MonoBehaviour
{
    [SerializeField] lv_spike_anim spikeAnim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //spikeAnim.enabled = true;
        spikeAnim.Init();
    }
}
