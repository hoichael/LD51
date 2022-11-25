using UnityEngine;

public class pl_spikecheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Spike"))
        {
            refs_global.Instance.levelManager.InitLevel(refs_global.Instance.levelManager.currentLevelIDX);
        }
    }
}
