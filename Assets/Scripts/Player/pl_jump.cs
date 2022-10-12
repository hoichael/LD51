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
        refs.rb.AddForce(Vector2.up * refs.settings.jumpForceBase, ForceMode2D.Impulse);
    }
}
