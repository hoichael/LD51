using UnityEngine;

public class pl_throw_teleport : MonoBehaviour
{
    [SerializeField] pl_refs refs;
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

        // dispose of ball
        Destroy(refs_global.Instance.currentBallRefs.trans.gameObject);
        refs_global.Instance.currentBallRefs = null;
    }
}
