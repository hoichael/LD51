using UnityEngine;

public class lv_forcepad : MonoBehaviour
{
    //[SerializeField] bool resetVelocity;
    [SerializeField, Range(0, 1)] float velResetMult;
    [SerializeField] float forcePlayer, forceBall;
    [SerializeField] Vector2 dir;

    [SerializeField] SpriteRenderer sprRenderer;
    [SerializeField] Color activationColor;
    [SerializeField] float colorLerpSpeed;
    [SerializeField] AnimationCurve colorLerpCurve;
    Color defaultColor;
    float currentColorLerpFactor;

    // this is kinda scuffed. maybe pl_events belongs in global refs? maybe some globally accessible wrapper? will depend on how many non-player systems can modify pl movement. hmm...
    //[SerializeField] pl_events playerEvents;

    private void Awake()
    {
        defaultColor = sprRenderer.color;
        currentColorLerpFactor = 1;
    }

    private void Update()
    {
        if (currentColorLerpFactor != 1) HandleColorLerp();
    }

    private void InitColorHandling()
    {
        sprRenderer.color = activationColor;
        currentColorLerpFactor = 0;
    }

    private void HandleColorLerp()
    {
        currentColorLerpFactor = Mathf.MoveTowards(currentColorLerpFactor, 1, colorLerpSpeed * Time.deltaTime);

        sprRenderer.color = Color.Lerp(
            activationColor,
            defaultColor,
            colorLerpCurve.Evaluate(currentColorLerpFactor)
            );
    }

    public void HandlePlayerContact()
    {
        //playerEvents.OnExitGround();
        //playerEvents.OnForcepad();
        refs_global.Instance.playerEvents.OnExitGround();
        refs_global.Instance.playerEvents.OnForcepad();

        refs_global.Instance.playerTrans.localPosition += new Vector3(dir.normalized.x * 0.2f, dir.normalized.y * 0.2f, 0);

        Rigidbody2D playerRB = refs_global.Instance.playerRB;

        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);

        ApplyForce(playerRB, forcePlayer);

        InitColorHandling();
    }

    public void HandleBallContact(Rigidbody2D rb)
    {
        rb.transform.position = new Vector3(transform.position.x, rb.transform.position.y, 0);
        rb.velocity = Vector2.zero;
        ApplyForce(rb, forceBall);

        InitColorHandling();
    }

    private void ApplyForce(Rigidbody2D rb, float forceToAdd)
    {
        //rb.velocity *= velResetMult;
        rb.AddForce(dir.normalized * forceToAdd, ForceMode2D.Impulse);
    }
}
