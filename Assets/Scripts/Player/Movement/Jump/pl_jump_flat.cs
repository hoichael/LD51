using UnityEngine;

public class pl_jump_flat : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    private void OnEnable()
    {
        ApplyForceBase();
    }

    private void FixedUpdate()
    {
        ApplyForceAdd();
    }

    private void ApplyForceBase()
    {
        // reset y vel before applying jump force for consistent jump
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, 0);
        //if (refs.rb.velocity.y < 0)
        //{
        //    refs.rb.velocity = new Vector2(refs.rb.velocity.x, 0);
        //}

        // handle main force
        refs.rb.AddForce(new Vector2(
            refs.rb.velocity.x * 0.1f,
            refs.settings.jump.forceBase
            ),
            ForceMode2D.Impulse);
    }

    private void ApplyForceAdd()
    {
        refs.rb.AddForce(new Vector2(
            refs.rb.velocity.x * 0.16f,
            refs.settings.jump.forceAdd
            ),
            ForceMode2D.Force);
    }
}
