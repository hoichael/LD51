using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_jump : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    private void Update()
    {
        if (refs.info.grounded && Input.GetKeyDown(KeyCode.Space)) ApplyForce();
    }

    private void ApplyForce()
    {
        // reset y vel before applying jump force for consistent jump
        refs.rb.velocity = new Vector2(refs.rb.velocity.x, 0);

        refs.rb.AddForce(Vector2.up * refs.settings.jumpForceBase, ForceMode2D.Impulse);
    }
}
