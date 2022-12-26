using UnityEngine;

public class pl_spikecheck : MonoBehaviour
{
    [SerializeField] pl_events events;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Spike"))
        {
            events.OnDeath();
            //refs_global.Instance.levelManager.InitLevel(refs_global.Instance.levelManager.currentLevelIDX);
        }
    }
}
