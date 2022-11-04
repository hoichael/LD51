//using UnityEngine;

//public class pl_throw_base : MonoBehaviour
//{
//    [SerializeField] protected pl_refs refs;
//    protected bool currentlyCharging;
//    protected float currentCharge;

//    protected Vector2 currentAimDir;

//    protected virtual void FixedUpdate()
//    {
//        if (!currentlyCharging) return;

//        currentCharge += refs.settings.throwForceAdd * Time.fixedDeltaTime;

//        if (currentCharge >= refs.settings.throwForceMax)
//        {
//            currentCharge = refs.settings.throwForceMax;
//            InitThrow();
//        }
//    }

//    protected virtual void InitThrow()
//    {
//        refs_global.Instance.ballInHand = currentlyCharging = false;
//        refs_global.Instance.currentBallRefs.manager.HandleThrow();

//        ApplyForce();
//    }

//    protected virtual void ApplyForce()
//    {
//        // apply main force based on player input
//        refs_global.Instance.currentBallRefs.rb.AddForce(currentAimDir.normalized * currentCharge, ForceMode2D.Impulse);

//        // if dir doesnt point downwards apply slight secondary upwards force
//        if (currentAimDir.y >= 0)
//        {
//            refs_global.Instance.currentBallRefs.rb.AddForce(Vector2.up * (currentCharge * 0.18f), ForceMode2D.Impulse);
//        }
//    }

//    protected void HandleTeleport()
//    {
//        // set player pos and vel to ball
//        refs.bodyTrans.position = refs_global.Instance.currentBallRefs.trans.position;
//        refs.rb.velocity = refs_global.Instance.currentBallRefs.rb.velocity * 1.15f;

//        // dispose of ball
//        Destroy(refs_global.Instance.currentBallRefs.trans.gameObject);
//        refs_global.Instance.currentBallRefs = null;
//    }
//}
