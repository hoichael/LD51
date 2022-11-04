using UnityEngine;

public class pl_throw_input_flat : MonoBehaviour
{
    [SerializeField] pl_throw_manager manager;
    private void Awake()
    {
        this.enabled = false;
    }

    private void Update()
    {
        if(refs_global.Instance.ip.I.Play.AimX.WasReleasedThisFrame() || refs_global.Instance.ip.I.Play.AimY.WasReleasedThisFrame())
        {
            manager.HandleThrow();
            return;
        }

        Vector2 input = new Vector2(refs_global.Instance.ip.I.Play.AimX.ReadValue<float>(), refs_global.Instance.ip.I.Play.AimY.ReadValue<float>());
        manager.currentAimDir = input.normalized;
    }
}
