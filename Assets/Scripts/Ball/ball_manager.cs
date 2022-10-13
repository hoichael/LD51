using UnityEngine;

public class ball_manager : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    [SerializeField] ball_throw thrower;
    bool ballInHand;

    bool currentlyChargingThrow;
    // public for debug
    public float throwForceCurrent;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0))
        {
            if(ballInHand)
            {
                currentlyChargingThrow = true;
                throwForceCurrent = refs.settings.throwForceBase;
                //HandleThrow();
            }
            else if(refs_global.Instance.currentBallTrans != null)
            {
                HandleTeleport();
            }
        }

        if(Input.GetKeyUp(KeyCode.K) || Input.GetMouseButtonUp(0))
        {
            // strange ifcheck but necessary to prevent rare but possible edge case
            // that should intuitively not be possible, but is,
            // and is easy to reproduce if tried to cause intentionally
            // whtv ¯\_(^.^)_/¯
            if (currentlyChargingThrow) HandleThrow();
        }
    }

    private void FixedUpdate()
    {
        if (!currentlyChargingThrow) return;

        throwForceCurrent += refs.settings.throwForceAdd * Time.fixedDeltaTime;

        if(throwForceCurrent >= refs.settings.throwForceMax)
        {
            throwForceCurrent = refs.settings.throwForceMax;
            HandleThrow();
        }
    }

    public void HandlePickup()
    {
        ballInHand = true;
        refs_global.Instance.currentBallTrans = refs.trans;
    }

    private void HandleThrow()
    {
        ballInHand = currentlyChargingThrow = false;

        Vector2 throwDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            ).normalized;

        if(throwDir == Vector2.zero)
        {
            throwDir = new Vector2(refs_global.Instance.playerDir, 0);
        }

        thrower.Init(throwDir, throwForceCurrent);
    }

    private void HandleTeleport()
    {
        // set player pos and vel to ball
        refs_global.Instance.playerTrans.position = refs.trans.position;
        refs_global.Instance.playerRB.velocity = refs.rb.velocity * 1.15f;

        // dispose of ball
        refs_global.Instance.currentBallTrans = null;
        Destroy(refs.trans.gameObject);
    }
}
