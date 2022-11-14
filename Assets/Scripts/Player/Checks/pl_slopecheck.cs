using UnityEngine;

public class pl_slopecheck : MonoBehaviour
{
    [SerializeField] pl_refs refs;

    private void Update()
    {
        if (!refs.info.grounded)
        {
            refs.info.slope = 0;
            //HandleSprite();
            return;
        }

        // 2 raycasts, slightly offset from center X of player
        RaycastHit2D hitA = Physics2D.Raycast(
            new Vector2(refs.groundcheckTrans.position.x - 0.35f, refs.groundcheckTrans.position.y + 0.25f), 
            Vector2.down, refs.settings.checks.slopeCheckLength, refs.settings.checks.solidLayer);
        RaycastHit2D hitB = Physics2D.Raycast(
            new Vector2(refs.groundcheckTrans.position.x - 0.35f, refs.groundcheckTrans.position.y + 0.25f),
            Vector2.down, refs.settings.checks.slopeCheckLength, refs.settings.checks.solidLayer);

        // handle raycast results
        EvaluateHit(hitA, hitB);
        //HandleSprite();
    }

    private void EvaluateHit(RaycastHit2D hitA, RaycastHit2D hitB)
    {
        int normalA = GetNormal(hitA);
        int normalB = GetNormal(hitB);

        if (normalA != 0)
        {
            refs.info.slope = normalA;
        }
        else if(normalB != 0)
        {
            refs.info.slope = normalB;
        }
        else
        {
            refs.info.slope = 0;
        }
    }

    private int GetNormal(RaycastHit2D hit)
    {
        if(hit == false)
        {
            return 0;
        }
        return Mathf.RoundToInt(hit.normal.x);
    }

    //private void HandleSprite()
    //{
    //    if (refs.info.slope == 0)
    //    {
    //        refs.FlipContainerTrans.localRotation = Quaternion.Euler(Vector3.zero);
    //        refs.FlipContainerTrans.localPosition = Vector3.zero;
    //    }
    //    else
    //    {
    //        refs.FlipContainerTrans.localRotation = Quaternion.Euler(0, 0, sprSlopeRotZ * -refs.info.slope);
    //        refs.FlipContainerTrans.localPosition = new Vector3(0, sprSlopeOffsetY, 0);
    //    }
    //}
}
