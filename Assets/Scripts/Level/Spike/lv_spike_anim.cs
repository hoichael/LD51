using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this whole class should be static / designed to be a single generic instance used by all spike instances instead of sitting on each spike instance - needless multiples
// - not static actually as it hooks into the update loop -> repeatedly calling anim func from update loop on spike instances would work (and be somewhat more optimized) but undercut the purpose of limiting duplicate code
// will work something out. this is fine for now. realistically, the performance implications dont matter anyway 

public class lv_spike_anim : MonoBehaviour
{
    [SerializeField] Color colorDef;
    [SerializeField] Color colorActive;
    [SerializeField] MeshRenderer mRenderer;
    Material mat;
    [SerializeField] float animSpeed;
    [SerializeField] AnimationCurve animCurve;

    float currentAnimFactor = 1;

    public void Init()
    {
        currentAnimFactor = 0;
        mat = mRenderer.material;
        this.enabled = true;
    }

    private void Update()
    {
        HandleAnim();
    }

    private void HandleAnim()
    {
        currentAnimFactor = Mathf.MoveTowards(currentAnimFactor, 1, animSpeed * Time.deltaTime);

        mat.color = Color.Lerp(
            colorActive,
            colorDef,
            animCurve.Evaluate(currentAnimFactor)
            );

        if (currentAnimFactor == 1) this.enabled = false;
    }

    private void OnDisable()
    {
        currentAnimFactor = 1;
        mat.color = colorDef;
        this.enabled = false; // necessary because script can be disabled on hierarchy level (parent disabled) while still being enabled itself -> inactive but OnEnable trigger upon parent enabled
    }
}
