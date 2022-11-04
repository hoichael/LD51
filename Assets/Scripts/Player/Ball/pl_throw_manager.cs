using UnityEngine;

public class pl_throw_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_throw_indicator indicator;
    [SerializeField] pl_throw_input_flat inputFlat;
    [SerializeField] pl_throw_input_stick inputStick;
    bool currentlyCharging;
    public float currentCharge;
    public Vector2 currentAimDir;

    private void Update()
    {
        if (currentlyCharging || !refs_global.Instance.ballInHand) return;

        //if (refs_global.Instance.ip.I.Play.AimStick.ReadValue<Vector2>() != Vector2.zero)
        if (refs_global.Instance.ip.I.Play.AimStick.ReadValue<Vector2>().sqrMagnitude > 0.49f)
        {
            currentlyCharging = true;
            inputStick.enabled = true;
            indicator.ToggleIndicatorVisibility(true);
        }
        else if(new Vector2(refs_global.Instance.ip.I.Play.AimX.ReadValue<float>(), refs_global.Instance.ip.I.Play.AimY.ReadValue<float>()) != Vector2.zero)
        {
            currentlyCharging = true;
            inputFlat.enabled = true;
            indicator.ToggleIndicatorVisibility(true);
        }
    }

    private void FixedUpdate()
    {
        if (currentlyCharging)
        {
            Charge();
            indicator.UpdateIndicator(currentAimDir, currentCharge);
        }
    }

    public void Charge()
    {
        currentCharge += refs.settings.throwForceAdd * Time.fixedDeltaTime;
        indicator.UpdateIndicator(currentAimDir, currentCharge);

        if (currentCharge >= refs.settings.throwForceMax)
        {
            currentCharge = refs.settings.throwForceMax;
            HandleThrow();
        }
    }

    public void HandleThrow()
    {
        currentlyCharging = inputFlat.enabled = inputStick.enabled = false;
        refs_global.Instance.ballInHand = false;
        refs_global.Instance.currentBallRefs.manager.HandleThrow();
        indicator.HandleThrow();
        ApplyForce();
        currentCharge = 0;
    }

    private void ApplyForce()
    {
        // apply main force based on player input
        refs_global.Instance.currentBallRefs.rb.AddForce(currentAimDir.normalized * currentCharge, ForceMode2D.Impulse);

        // if dir doesnt point downwards apply slight secondary upwards force
        if (currentAimDir.y >= 0)
        {
            refs_global.Instance.currentBallRefs.rb.AddForce(Vector2.up * (currentCharge * 0.18f), ForceMode2D.Impulse);
        }
    }
}
