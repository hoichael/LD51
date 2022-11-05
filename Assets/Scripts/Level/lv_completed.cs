using System.Collections;
using UnityEngine;

public class lv_completed : MonoBehaviour
{
    [SerializeField] Transform uiContainer;
    [SerializeField] menu_navigate navigator;
    [SerializeField] float uiShowDelay;

    [SerializeField] float uiTransitionSpeed;
    [SerializeField] AnimationCurve uiTransitionCurve;
    Vector3 containerHiddenPos;
    bool inUITransition;
    float currentTransitionFactor;

    private void Start()
    {
        containerHiddenPos = uiContainer.localPosition;
        navigator.enabled = false;
    }

    private void Update()
    {
        if (inUITransition) HandleShowUITransition();
    }

    public void Init()
    {
        StartCoroutine(ShowUI());
    }

    private void HandleShowUITransition()
    {
        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, 1, uiTransitionSpeed * Time.deltaTime);

        uiContainer.localPosition = Vector3.Lerp(
            containerHiddenPos,
            Vector3.zero,
            uiTransitionCurve.Evaluate(currentTransitionFactor)
            );
    }

    private IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(uiShowDelay);
        currentTransitionFactor = 0;
        inUITransition = true;
    }
}
