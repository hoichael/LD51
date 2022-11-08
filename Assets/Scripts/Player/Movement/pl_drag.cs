using UnityEngine;

public class pl_drag : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] float dragResetSpeed;
    [SerializeField] float walljumpDrag;
    [SerializeField] pl_gravity grav;

    bool inTeleportTransition;
    public float tpFactor;
    public float currentTransitionFactor;
    bool ignoreMove;

    //private void Update()
    //{
    //    if(refs_global.Instance.currentBallRefs != null)
    //    {
    //        if(refs_global.Instance.currentBallRefs.rb != null)
    //        print(refs_global.Instance.currentBallRefs.rb.velocity.magnitude);
    //    }    
    //}

    public void HandleWalljump()
    {
        refs.rb.drag = walljumpDrag;
        refs.info.moveForceCurrent = refs.settings.moveForceAir;
        inTeleportTransition = false;
    }

    public void HandleTeleport(Vector2 ballVel)
    {
        inTeleportTransition = true;
        refs.rb.drag = 0.1f;
        refs.info.moveForceCurrent = 0;
        ignoreMove = false;

        float mag = new Vector2(ballVel.x, ballVel.y * 0.16f).magnitude;
        tpFactor = Mathf.Clamp(60 - mag, 0.1f, 60);
        tpFactor = (tpFactor / 60) * 2.1f;

        if(Mathf.Abs(ballVel.x) < 1)
        {
            refs.info.moveForceCurrent = refs.settings.moveForceAir;
            ignoreMove = true;
        }

        grav.gravCurrent = Mathf.Clamp((((60 - ballVel.magnitude) / 60) * refs.settings.gravBase) * 2, 70, 125);

        currentTransitionFactor = 0;
    }

    private void FixedUpdate()
    {
        //float target = refs.info.grounded ? refs.settings.dragGround : refs.settings.dragAir;
        //refs.rb.drag = Mathf.MoveTowards(refs.rb.drag, target, dragResetSpeed * Time.fixedDeltaTime);
        if(refs.info.grounded)
        {
            refs.rb.drag = refs.settings.dragGround;
            refs.info.moveForceCurrent = refs.settings.moveForceGround;
            inTeleportTransition = false;
        }
        else if(inTeleportTransition)
        {
            if(refs.rb.velocity.magnitude < 4)
            {
                refs.rb.drag = refs.settings.dragAir;
                refs.info.moveForceCurrent = refs.settings.moveForceAir;
                inTeleportTransition = false;
                return;
            }

            HandleTeleportTransition();
        }
        else 
        {
            refs.rb.drag = Mathf.MoveTowards(refs.rb.drag, refs.settings.dragAir, dragResetSpeed * Time.fixedDeltaTime);
        }
    }

    private void HandleTeleportTransition()
    {
        //refs.rb.drag = Mathf.MoveTowards(refs.rb.drag, refs.settings.dragAir, tpFactor * Time.fixedDeltaTime);
        //refs.info.moveForceCurrent = Mathf.MoveTowards(refs.info.moveForceCurrent, refs.settings.moveForceAir, tpFactor * Time.fixedDeltaTime);

        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, 1, tpFactor * Time.fixedDeltaTime);

        refs.rb.drag = Mathf.Lerp(
            0,
            refs.settings.dragAir,
            currentTransitionFactor
            );

        if (ignoreMove) return;
        refs.info.moveForceCurrent = Mathf.Lerp(
            0,
            refs.settings.moveForceAir,
            currentTransitionFactor
            );
    }
}
