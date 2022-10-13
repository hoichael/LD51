using UnityEngine;

public class pl_gravity : MonoBehaviour
{
    [SerializeField] pl_refs refs;

    // public for debug
    public float gravCurrent;

    private void OnEnable()
    {
        gravCurrent = refs.settings.gravBase;
    }

    private void FixedUpdate()
    {
        // continuously increase current grav
        Mathf.Clamp(gravCurrent += refs.settings.gravAdd * Time.fixedDeltaTime, 0, refs.settings.gravMax);

        refs.rb.AddForce(Vector2.down * gravCurrent, ForceMode2D.Force);
    }
}
