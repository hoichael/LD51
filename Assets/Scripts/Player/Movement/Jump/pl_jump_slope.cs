using UnityEngine;

public class pl_jump_slope : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    Vector2 jumpDir;

    private void OnEnable()
    {
        SetJumpDir();
        ApplyForceBase();
    }

    private void FixedUpdate()
    {
        ApplyForceAdd();
    }

    private void SetJumpDir()
    {
        int slopeDir = refs.info.slope;
        int inputDir = (int)refs_global.Instance.ip.I.Play.Move.ReadValue<float>();

        if(inputDir == 0)
        {
            jumpDir = new Vector2(0.65f * slopeDir, 0.65f);
        }
        else if(inputDir == -slopeDir)
        {
            jumpDir = new Vector2(0, 0.92f);
        }
        else // inputDir == slopeDir
        {
            jumpDir = new Vector2(0.6f * slopeDir, 0.24f);
        }
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
        refs.rb.AddForce(jumpDir * refs.settings.jump.slopeForceBase, ForceMode2D.Impulse);
    }

    private void ApplyForceAdd()
    {
        refs.rb.AddForce((jumpDir * refs.settings.jump.slopeForceAdd) * Time.deltaTime, ForceMode2D.Force);
    }
}
