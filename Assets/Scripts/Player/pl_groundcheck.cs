using UnityEngine;

public class pl_groundcheck : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] LayerMask groundMask;

    private void Update()
    {
        CheckForGround();
    }

    private void CheckForGround()
    {
        if(Physics2D.OverlapBox(refs.groundcheckTrans.position, refs.settings.groundcheckSize, 0, groundMask) == null)
        {
            refs.gravity.enabled = true;
            refs.info.grounded = false;
        }
        else
        {
            refs.gravity.enabled = false;
            refs.info.grounded = true;
        }
    }
}
