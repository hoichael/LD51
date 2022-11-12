using UnityEngine;

public class lv_forcepad : MonoBehaviour
{
    //[SerializeField] bool resetVelocity;
    [SerializeField, Range(0, 1)] float velResetMult;
    [SerializeField] float forcePlayer, forceBall;
    [SerializeField] Vector2 dir;

    // this is kinda scuffed. maybe pl_events belongs in global refs? maybe some globally accessible wrapper? will depend on how many non-player systems can modify pl movement. hmm...
    [SerializeField] pl_events playerEvents;

    public void HandlePlayerContact()
    {
        print("sdfsd");
        playerEvents.OnExitGround();
        playerEvents.OnForcepad();

        refs_global.Instance.playerTrans.localPosition += new Vector3(dir.normalized.x * 0.2f, dir.normalized.y * 0.2f, 0);

        Rigidbody2D playerRB = refs_global.Instance.playerRB;

        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);

        ApplyForce(playerRB, forcePlayer);
    }

    public void HandleBallContact(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
        ApplyForce(rb, forceBall);
    }

    private void ApplyForce(Rigidbody2D rb, float forceToAdd)
    {
        //rb.velocity *= velResetMult;
        rb.AddForce(dir.normalized * forceToAdd, ForceMode2D.Impulse);
    }
}
