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

        RaycastHit2D hitLeft = Physics2D.Raycast(
            new Vector2(refs.groundcheckTrans.position.x - refs.settings.checks.slopeCheckOffsetHor, refs.bodyTrans.position.y), 
            Vector2.down, refs.settings.checks.slopeCheckLength, refs.settings.checks.solidLayer);

        RaycastHit2D hitMid = Physics2D.Raycast(
            refs.bodyTrans.position,
            Vector2.down, refs.settings.checks.slopeCheckLength, refs.settings.checks.solidLayer);

        RaycastHit2D hitRight = Physics2D.Raycast(
            new Vector2(refs.groundcheckTrans.position.x + refs.settings.checks.slopeCheckOffsetHor, refs.bodyTrans.position.y),
            Vector2.down, refs.settings.checks.slopeCheckLength, refs.settings.checks.solidLayer);

        EvaluateHits(hitLeft, hitMid, hitRight);
    }

    // bad algorithm - will write proper implementation later
    // also: no proper handling of null hits yet (-> (literal) edge cases)
    private void EvaluateHits(RaycastHit2D hitLeft, RaycastHit2D hitMid, RaycastHit2D hitRight)
    {
        int normalLeft = GetNormal(hitLeft);
        int normalMid = GetNormal(hitMid);
        int normalRight = GetNormal(hitRight);

        if (normalLeft != 0 && normalRight != 0)
        {
            if (normalMid == 0 || normalLeft != normalRight)
            {
                refs.info.slope = 0;
                return;
            }
        }

        if (normalMid == 0)
        {
            refs.info.slope = 0;
        }
        else
        {
            if (normalMid == normalLeft)
            {
                if(normalMid == normalRight || hitRight.point.y <= hitMid.point.y)
                {
                    refs.info.slope = normalMid;
                }
            }
            else if (normalMid == normalRight)
            {
                if (normalMid == normalLeft || hitLeft.point.y <= hitMid.point.y)
                {
                    refs.info.slope = normalMid;
                }
            }
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawLine(
    //        new Vector2(refs.groundcheckTrans.position.x - refs.settings.checks.slopeCheckOffsetHor, refs.bodyTrans.position.y),
    //        new Vector2(refs.groundcheckTrans.position.x - refs.settings.checks.slopeCheckOffsetHor, refs.bodyTrans.position.y - refs.settings.checks.slopeCheckLength));

    //    Gizmos.DrawLine(
    //        refs.bodyTrans.position,
    //        new Vector2(refs.bodyTrans.position.x, refs.bodyTrans.position.y - refs.settings.checks.slopeCheckLength));

    //    Gizmos.DrawLine(
    //       new Vector2(refs.groundcheckTrans.position.x + refs.settings.checks.slopeCheckOffsetHor, refs.bodyTrans.position.y),
    //        new Vector2(refs.groundcheckTrans.position.x + refs.settings.checks.slopeCheckOffsetHor, refs.bodyTrans.position.y - refs.settings.checks.slopeCheckLength));
    //}
}
