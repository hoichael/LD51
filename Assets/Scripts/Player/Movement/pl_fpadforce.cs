using System.Collections;
using UnityEngine;

public class pl_fpadforce : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] float force;
    [SerializeField] float duration;
    [SerializeField, Range(0.95f, 1)] float xVelMult;

    bool active;

    private void FixedUpdate()
    {
        if (active) HandleForce();
    }

    private void HandleForce()
    {
        if(xVelMult != 1) // if check doesnt really do anything but is probably more performant. . . or less performant because additional check. hmm. (none of this matters lole)
        {
            xVelMult = 0.99f; //temp
            refs.rb.velocity = new Vector2(refs.rb.velocity.x * xVelMult, refs.rb.velocity.y);
        }

        refs.rb.AddForce(Vector2.up * (/* temp: */ force / 50), ForceMode2D.Force);
    }

    public void Init()
    {
        active = true;
        StartCoroutine(HandleDuration());
    }

    public void Cancel()
    {
        active = false;
        StopAllCoroutines();
    }

    private IEnumerator HandleDuration()
    {
        yield return new WaitForSeconds(duration);
        active = false;
    }
}
