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

    [SerializeField] GameObject starTwoToggle, starThreeToggle;
    [SerializeField] TextMeshPro starTwoText, starThreeText;

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

    public void Init(string finalTimeAsString, float finalTimeInSeconds, lv_info levelInfo)
    {
        timeText.text = finalTimeAsString;
        StartCoroutine(ShowUI());
        HandleStarsText(levelInfo);
        HandleStarsTime(finalTimeInSeconds, levelInfo);
    }

    public void Reset()
    {
        StopAllCoroutines();
        navigator.enabled = false;
        inUITransition = false;
        uiContainer.localPosition = containerHiddenPos;
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

    private void HandleStarsText(lv_info levelInfo)
    {
        string starTwoTimeAsString = $"0 : {levelInfo.timeTwoStars.seconds} : {levelInfo.timeTwoStars.milliseconds}";
        string starThreeTimeAsString = $"0 : {levelInfo.timeThreeStars.seconds} : {levelInfo.timeThreeStars.milliseconds}";

        starTwoText.text = starTwoTimeAsString;
        starThreeText.text = starThreeTimeAsString;
    }

    private void HandleStarsTime(float finalTimeInSeconds, lv_info levelInfo)
    {
        if(finalTimeInSeconds <= levelInfo.timeThreeStars.seconds + (float)(levelInfo.timeThreeStars.milliseconds * 0.001f))
        {
            starThreeToggle.SetActive(true);
            starTwoToggle.SetActive(true);
        }
        else
        {
            starThreeToggle.SetActive(false);
            if(finalTimeInSeconds <= levelInfo.timeTwoStars.seconds + (float)(levelInfo.timeTwoStars.milliseconds * 0.001f))
            {
                starTwoToggle.SetActive(true);
            }
            else
            {
                starTwoToggle.SetActive(false);
            }
        }
    }

    private IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(uiShowDelay);
        currentTransitionFactor = 0;
        inUITransition = true;
        navigator.enabled = true;
        navigator.SwitchSelection(initSelectable);
    }
}
