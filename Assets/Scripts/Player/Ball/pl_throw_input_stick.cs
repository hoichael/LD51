using UnityEngine;

public class pl_throw_input_stick : MonoBehaviour
{
    [SerializeField] pl_throw_manager manager;
    private void Awake()
    {
        this.enabled = false;
    }

    private void Update()
    {
        //if (refs_global.Instance.ip.I.Play.AimStick.ReadValue<Vector2>() == Vector2.zero)
        //if (refs_global.Instance.ip.I.Play.AimStick.WasReleasedThisFrame())
        if(refs_global.Instance.ip.I.Play.AimStick.ReadValue<Vector2>().sqrMagnitude < 0.47f)
        {
            manager.HandleThrow();
            return;
        }

        manager.currentAimDir = ProcessInput(refs_global.Instance.ip.I.Play.AimStick.ReadValue<Vector2>());
    }

    private Vector2 ProcessInput(Vector2 input)
    {
        return new Vector2(Round(input.x), Round(input.y)).normalized;
    }

    private float Round(float num)
    {
        if (num == 0) return 0;
        int mult = num > 0 ? 1 : -1;

        if (Mathf.Abs(num) > 0.5f)
        {
            return 1 * mult;
        }
        return 0;
    }
}
