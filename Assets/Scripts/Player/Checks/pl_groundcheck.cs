using UnityEngine;

public class pl_groundcheck : MonoBehaviour
{
    [SerializeField] pl_refs refs;

    [SerializeField] pl_bodydeform bodyDeform; // this is terrible lol
    [SerializeField] pl_bodyturn bodyTurn; // this is terrible lol
    float mostRecentVelY; // this is terrible lol

    private void Update()
    {
        CheckForGround();
        mostRecentVelY = refs.rb.velocity.y != 0 ? refs.rb.velocity.y : mostRecentVelY; // this is terrible lol
    }

    private void CheckForGround()
    {
        // inefficient but fine for now
        if(Physics2D.OverlapBox(refs.groundcheckTrans.position, refs.settings.checks.groundCheckSize, 0, refs.settings.checks.solidLayer) == null)
        {
            if(refs.info.grounded)
            {
                //refs.gravity.enabled = true;
                //refs.info.grounded = false;
                //refs.rb.drag = refs.settings.dragAir;
                //refs.info.moveForceCurrent = refs.settings.moveForceAir;

                refs.events.OnExitGround();
            }
        }
        else
        {
            if(!refs.info.grounded)
            {
                //refs.gravity.enabled = false;
                //refs.info.grounded = true;
                //refs.rb.drag = refs.settings.dragGround;
                //refs.info.moveForceCurrent = refs.settings.moveForceGround;

                refs.events.OnEnterGround();
                bodyDeform.OnLand(mostRecentVelY); // this is terrible lol
                bodyTurn.OnLand(mostRecentVelY); // this is terrible lol
            }
        }
    }
}
