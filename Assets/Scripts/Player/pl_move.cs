using UnityEngine;

public class pl_move : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    float currentInput;

    private void Update()
    {
        currentInput = (int)Input.GetAxisRaw("Horizontal");

        // update info dir and flip sprite accordingly
        if (currentInput != 0)
        {
            refs.info.dir = refs_global.Instance.playerDir = (int)currentInput;
            refs.spriteTrans.localScale = new Vector3(refs.info.dir, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (currentInput == 0) return;
        ApplyForce();
    }

    private void ApplyForce()
    {
        refs.rb.AddForce(Vector2.right * refs.info.moveForceCurrent * currentInput, ForceMode2D.Force);
    }
}
