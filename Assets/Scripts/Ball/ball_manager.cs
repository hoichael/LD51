using System.Collections;
using UnityEngine;

public class ball_manager : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    [SerializeField] ball_throw thrower;
    bool ballInHand;

    bool currentlyChargingThrow;
    // public for debug
    public float throwForceCurrent;

    float lastAimX, lastAimY;


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0))s
    //    {
    //        if(ballInHand)
    //        {
    //            currentlyChargingThrow = true;
    //            throwForceCurrent = refs.settings.throwForceBase;
    //        }
    //        else if(refs_global.Instance.currentBallTrans == refs.trans)
    //        {
    //            HandleTeleport();
    //        }
    //    }

    //    if(Input.GetKeyUp(KeyCode.K) || Input.GetMouseButtonUp(0))
    //    {
    //        // strange ifcheck but necessary to prevent rare but possible edge case
    //        // that should intuitively not be possible, but is,
    //        // and is easy to reproduce if tried to cause intentionally
    //        // whtv ¯\_(^.^)_/¯
    //        if (currentlyChargingThrow) HandleThrow();
    //    }
    //}

    //private void Update()
    //{
    //    if(!ballInHand && Input.GetKeyDown(KeyCode.LeftShift))
    //    {
    //        if (refs_global.Instance.currentBallTrans == refs.trans)
    //        {
    //            HandleTeleport();
    //        }
    //    }

    //    //if (currentlyChargingThrow)
    //    //{
    //    //    if (Input.GetKeyDown(KeyCode.LeftShift))
    //    //    {
    //    //        HandleThrow();
    //    //    }
    //    //    lastAimX = Input.GetAxisRaw("Aim Horizontal");
    //    //    lastAimY = Input.GetAxisRaw("Aim Vertical");
    //    //}
    //    if (currentlyChargingThrow)
    //    {
    //        if (Input.GetKeyUp(KeyCode.UpArrow) ||
    //            Input.GetKeyUp(KeyCode.DownArrow) ||
    //            Input.GetKeyUp(KeyCode.LeftArrow) ||
    //            Input.GetKeyUp(KeyCode.RightArrow))
    //        {
    //            HandleThrow();
    //        }
    //        lastAimX = Input.GetAxisRaw("Aim Horizontal");
    //        lastAimY = Input.GetAxisRaw("Aim Vertical");
    //    }
    //    else if (Input.GetAxisRaw("Aim Horizontal") != 0 || Input.GetAxisRaw("Aim Vertical") != 0)
    //    {
    //        if (ballInHand)
    //        {
    //            currentlyChargingThrow = true;
    //            throwForceCurrent = refs.settings.throwForceBase;
    //        }
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    if (!currentlyChargingThrow) return;

    //    throwForceCurrent += refs.settings.throwForceAdd * Time.fixedDeltaTime;

    //    if(throwForceCurrent >= refs.settings.throwForceMax)
    //    {
    //        throwForceCurrent = refs.settings.throwForceMax;
    //        HandleThrow();
    //    }
    //}

    public void HandlePickup()
    {
        //ballInHand = true;
        refs_global.Instance.ballInHand = true;
        //refs_global.Instance.currentBallTrans = refs.trans;
        //refs_global.Instance.currentHeldBallObj = refs.trans.gameObject;
    }

    //public bool CurrentlyHoldingBall()
    //{
    //    return ballInHand;
    //}

    public void HandleThrow()
    {
        refs.trans.SetParent(null);
        refs.colSolid.enabled = true;
        refs.trail.emitting = true;

        CreateRB();

        StartCoroutine(DelayedColHandler());
    }

    private void CreateRB()
    {
        Rigidbody2D rb = refs.trans.gameObject.AddComponent<Rigidbody2D>();
        rb.sharedMaterial = refs.settings.rbPhMat;
        rb.gravityScale = refs.settings.rbGravityScale;
        rb.collisionDetectionMode = refs.settings.rbColDetectMode;
        rb.interpolation = refs.settings.rbInterp;

        refs.rb = rb;
    }

    private IEnumerator DelayedColHandler()
    {
        yield return new WaitForSeconds(refs.settings.colEnableDelay);
        refs.colTrigger.enabled = true;
    }

    //private void HandleTeleport()
    //{
    //    // set player pos and vel to ball
    //    refs_global.Instance.playerTrans.position = refs.trans.position;
    //    refs_global.Instance.playerRB.velocity = refs.rb.velocity * 1.15f;

    //    // dispose of ball
    //    refs_global.Instance.currentBallTrans = null;
    //    Destroy(refs.trans.gameObject);
    //}
}
