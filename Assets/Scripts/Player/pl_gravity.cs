using UnityEngine;

public class pl_gravity : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    private void FixedUpdate()
    {
        refs.rb.AddForce(Vector2.down * refs.settings.gravBase, ForceMode2D.Force);
    }
}
