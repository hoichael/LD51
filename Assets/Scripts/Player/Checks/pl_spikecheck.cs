using UnityEngine;
using System.Collections;

public class pl_spikecheck : MonoBehaviour
{
    [SerializeField] pl_events events;
    bool hasTouchedSpikeThisFrame;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (hasTouchedSpikeThisFrame) return;

        if (col.CompareTag("Spike"))
        {
            lv_spike_col spikeColHandler;
            col.gameObject.TryGetComponent<lv_spike_col>(out spikeColHandler);
            //col.gameObject.GetComponent<lv_spike_col>().HandlePlayerCol();
            spikeColHandler?.HandleCol();

            StartCoroutine(LimitColHandlingToSingleSpike());

            events.OnDeath();

            //refs_global.Instance.levelManager.InitLevel(refs_global.Instance.levelManager.currentLevelIDX);
        }
    }

    private IEnumerator LimitColHandlingToSingleSpike()
    {
        hasTouchedSpikeThisFrame = true;
        yield return new WaitForSeconds(0);
        hasTouchedSpikeThisFrame = false;
    }
}
