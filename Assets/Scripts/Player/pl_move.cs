using UnityEngine;

public class pl_move : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    int currentInput;
    Vector2 processedDir;
    float slopeMult;

    private void Update()
    {
        //currentInput = (int)Input.GetAxisRaw("Horizontal");
        currentInput = (int)refs_global.Instance.ip.I.Play.Move.ReadValue<float>();

        // update info dir and flip sprite accordingly
        if (currentInput != 0)
        {
            refs.info.dir = refs_global.Instance.playerDir = (int)currentInput;
            refs.FlipContainerTrans.localScale = new Vector3(refs.info.dir, 1, 1);
        }
    }

    private void HandleGroundAngle(int input)
    {
        if (!refs.info.grounded || refs.info.slope == 0)
        {
            slopeMult = 1;
            processedDir = Vector2.right * input;
            return;
        }

        if(Mathf.Sign(refs.info.slope) == Mathf.Sign(currentInput))
        {
            slopeMult = 1.6f;
        }
        else
        {
            slopeMult = 0.78f;
        }

        processedDir = refs.info.slope > 0 ? new Vector2(input, -input).normalized : new Vector2(input, input).normalized;
    }

    private void FixedUpdate()
    {
        if (currentInput == 0) return;

        HandleGroundAngle(currentInput);
        ApplyForce();
    }

    private void ApplyForce()
    {
        // apply base force
        refs.rb.AddForce((processedDir * refs.info.moveForceCurrent) * slopeMult, ForceMode2D.Force);

        // if air turn, apply add force
        if(!refs.info.grounded && Mathf.Sign(currentInput) != Mathf.Sign(refs.rb.velocity.x))
        {
            refs.rb.AddForce(Vector2.right * refs.settings.moveForceAddAirTurn * currentInput, ForceMode2D.Force);
        }
    }
}
