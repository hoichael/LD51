using UnityEngine;

public class ball_manager : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    [SerializeField] ball_throw thrower;
    bool currentlyHeld;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0))
        {
            if(currentlyHeld)
            {
                HandleThrow();
            }
            else if(refs_global.Instance.currentBallTrans != null)
            {
                HandleTeleport();
            }
        }
    }

    public void HandlePickup()
    {
        currentlyHeld = true;
        refs_global.Instance.currentBallTrans = refs.trans;
    }

    private void HandleThrow()
    {
        currentlyHeld = false;

        Vector2 throwDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            ).normalized;

        if(throwDir == Vector2.zero)
        {
            throwDir = new Vector2(refs_global.Instance.playerDir, 0);
        }

        thrower.Init(throwDir);
    }

    private void HandleTeleport()
    {
        // set player pos and vel to ball
        refs_global.Instance.playerTrans.position = refs.trans.position;
        refs_global.Instance.playerRB.velocity = refs.rb.velocity;

        // dispose of ball
        refs_global.Instance.currentBallTrans = null;
        Destroy(refs.trans.gameObject);
    }
}
