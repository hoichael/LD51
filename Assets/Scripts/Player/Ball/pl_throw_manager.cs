using UnityEngine;

public class pl_throw_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] pl_throw_indicator indicator;
    [SerializeField] pl_throw_input_flat inputFlat;
    [SerializeField] pl_throw_input_stick inputStick;
    [SerializeField] lv_pool pool;
    bool currentlyCharging;

    float currentChargeFloat;
    public int currentChargeStep; //public for db

    public Vector2 currentAimDir;

    private void Update()
    {
        if(currentlyCharging)
        {
            indicator.UpdateRotation(currentAimDir);
            return;
        }

        if(!refs_global.Instance.ballInHand)
        {
            return;
        }

        //if (refs_global.Instance.ip.I.Play.AimStick.ReadValue<Vector2>() != Vector2.zero)
        if (refs_global.Instance.ip.I.Play.AimStick.ReadValue<Vector2>().sqrMagnitude > 0.49f)
        {
            currentlyCharging = true;
            inputStick.enabled = true;
            //indicator.ToggleIndicatorVisibility(true);
            indicator.InitCharge();
        }
        else if(new Vector2(refs_global.Instance.ip.I.Play.AimX.ReadValue<float>(), refs_global.Instance.ip.I.Play.AimY.ReadValue<float>()) != Vector2.zero)
        {
            currentlyCharging = true;
            inputFlat.enabled = true;
            //indicator.ToggleIndicatorVisibility(true);
            indicator.InitCharge();
        }
    }

    private void FixedUpdate()
    {
        if (currentlyCharging)
        {
            Charge();
            //indicator.UpdateIndicator(currentAimDir, currentCharge);
        }
    }


    private void Charge()
    {
        if (currentChargeFloat >= refs.settings.ballThrow.chargeCounterBreakpoints[3])
        {
            //currentChargeFloat = refs.settings.ballThrow.chargeCounterMax;
            currentChargeStep = 3;
            //HandleThrow();
            return;
        }

        currentChargeFloat += refs.settings.ballThrow.chargeCounterAdd * Time.fixedDeltaTime;
        if(currentChargeFloat >= refs.settings.ballThrow.chargeCounterBreakpoints[currentChargeStep + 1])
        {
            currentChargeStep++;
            indicator.HandleNewStep();
        }
        //indicator.UpdateIndicator(currentAimDir, currentCharge);
    }

    public void Cancel()
    {
        Reset();
        indicator.HandleThrow();
    }

    private void Reset()
    {
        currentlyCharging = inputFlat.enabled = inputStick.enabled = false;
        currentChargeFloat = currentChargeStep = 0;
    }

    public void HandleThrow()
    {
        refs_global.Instance.ballInHand = false;

        pool.Return(lv_pool.PoolType.Ball, refs_global.Instance.currentBallRefs.trans, true); // this is fucky

        refs_global.Instance.currentBallRefs.manager.HandleThrow();
        indicator.HandleThrow();

        ApplyForce();
        Reset();
    }

    private void ApplyForce()
    {
        // apply main force based on player input
        //refs_global.Instance.currentBallRefs.rb.AddForce(currentAimDir.normalized * currentChargeFloat, ForceMode2D.Impulse);
        refs_global.Instance.currentBallRefs.rb.AddForce(currentAimDir.normalized * refs.settings.ballThrow.forceSteps[currentChargeStep], ForceMode2D.Impulse);

        // if dir doesnt point downwards apply slight secondary upwards force
        if (currentAimDir.y >= 0)
        {
            //refs_global.Instance.currentBallRefs.rb.AddForce(Vector2.up * (currentChargeFloat * 0.18f), ForceMode2D.Impulse);
            refs_global.Instance.currentBallRefs.rb.AddForce(Vector2.up * (refs.settings.ballThrow.forceSteps[currentChargeStep] * 0.18f), ForceMode2D.Impulse);
        }
    }
}
