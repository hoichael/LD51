using UnityEngine;

public class pl_groundcheck : MonoBehaviour
{
    [SerializeField] pl_refs refs;

    private void Update()
    {
        CheckForGround();
    }

    private void CheckForGround()
    {
        // inefficient but fine for now
        if(Physics2D.OverlapBox(refs.groundcheckTrans.position, refs.settings.groundcheckSize, 0, refs.settings.solidLayer) == null)
        {
            refs.gravity.enabled = true;
            refs.info.grounded = false;
            refs.rb.drag = refs.settings.dragAir;
            refs.info.moveForceCurrent = refs.settings.moveForceAir;
        }
        else
        {
            refs.gravity.enabled = false;
            refs.info.grounded = true;
            refs.rb.drag = refs.settings.dragGround;
            refs.info.moveForceCurrent = refs.settings.moveForceGround;
        }
    }
}
