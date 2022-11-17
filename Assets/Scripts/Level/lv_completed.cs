using System.Collections;
using UnityEngine;
using TMPro;

public class lv_completed : MonoBehaviour
{
    [SerializeField] Transform uiContainer;
    [SerializeField] menu_navigate navigator;
    [SerializeField] float uiShowDelay;

    [SerializeField] float uiTransitionSpeed;
    [SerializeField] AnimationCurve uiTransitionCurve;
    Vector3 containerHiddenPos = new Vector3(0, -54, 0);
    bool inUITransition;
    float currentTransitionFactor;

    [SerializeField] menu_selectable initSelectable;

    public TextMeshPro timeText;

    private void Awake()
    {
        containerHiddenPos = uiContainer.localPosition;
        Reset();
    }

    private void Update()
    {
        if (inUITransition) HandleShowUITransition();
    }

    public void Init(string finalTime)
    {
        timeText.text = finalTime;
        navigator.enabled = true;
        navigator.SwitchSelection(initSelectable);
        StartCoroutine(ShowUI());
    }

    public void Reset()
    {
        uiContainer.localPosition = containerHiddenPos;
        navigator.enabled = false;
    }


    private void HandleShowUITransition()
    {
        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, 1, uiTransitionSpeed * Time.deltaTime);

        uiContainer.localPosition = Vector3.Lerp(
            containerHiddenPos,
            Vector3.zero,
            uiTransitionCurve.Evaluate(currentTransitionFactor)
            );

        if (currentTransitionFactor == 1) inUITransition = false;
    }

    private IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(uiShowDelay);
        currentTransitionFactor = 0;
        inUITransition = true;
    }
}
