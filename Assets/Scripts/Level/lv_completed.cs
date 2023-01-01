using System.Collections;
using UnityEngine;
using TMPro;

public class lv_completed : MonoBehaviour
{
    [SerializeField] bool DEV_ignorevisuals;

    [SerializeField] Transform uiContainer;
    [SerializeField] menu_navigate navigator;
    [SerializeField] float uiShowDelay;

    [SerializeField] float uiTransitionSpeed;
    [SerializeField] AnimationCurve uiTransitionCurve;
    Vector3 containerHiddenPos = new Vector3(0, -72, -5);
    Vector3 containerActivePos = new Vector3(0, 0, -5);
    bool inUITransition;
    float currentTransitionFactor;

    [SerializeField] menu_selectable initSelectable;

    //[SerializeField] GameObject starTwoToggle, starThreeToggle;
    [SerializeField] SpriteRenderer sprRendererStarTwo, sprRendererStarThree;
    [SerializeField] Color colorDefaultStar;
    [SerializeField] Material matStarTextDefault, matStarTextActive;

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
        timeText.text = "Completion Time : " + "<color=#ffffff>" + finalTimeAsString;
        StartCoroutine(ShowUI());

        if (DEV_ignorevisuals) return;
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
            containerActivePos,
            uiTransitionCurve.Evaluate(currentTransitionFactor)
            );

        if (currentTransitionFactor == 1) inUITransition = false;
    }

    private void HandleStarsText(lv_info levelInfo)
    {
        string starTwoTimeAsString = $"0 : {levelInfo.timeTwoStars.seconds} : {GetMillisecString(levelInfo.timeTwoStars.milliseconds)}";
        string starThreeTimeAsString = $"0 : {levelInfo.timeThreeStars.seconds} : {levelInfo.timeThreeStars.milliseconds}";

        starTwoText.text = starTwoTimeAsString;
        starThreeText.text = starThreeTimeAsString;
    }

    private void HandleStarsTime(float finalTimeInSeconds, lv_info levelInfo)
    {
        if(finalTimeInSeconds <= levelInfo.timeThreeStars.seconds + (float)(levelInfo.timeThreeStars.milliseconds * 0.001f))
        {
            //starThreeToggle.SetActive(true);
            //starTwoToggle.SetActive(true);
            sprRendererStarThree.color = sprRendererStarTwo.color = Color.white;
            starThreeText.fontMaterial = starTwoText.fontMaterial = matStarTextActive;
        }
        else
        {
            //starThreeToggle.SetActive(false);
            sprRendererStarThree.color = colorDefaultStar;
            starThreeText.fontMaterial = matStarTextDefault;
            if (finalTimeInSeconds <= levelInfo.timeTwoStars.seconds + (float)(levelInfo.timeTwoStars.milliseconds * 0.001f))
            {
                //starTwoToggle.SetActive(true);
                sprRendererStarTwo.color = Color.white;
                starTwoText.fontMaterial = matStarTextActive;
            }
            else
            {
                //starTwoToggle.SetActive(false);
                sprRendererStarTwo.color = colorDefaultStar;
                starTwoText.fontMaterial = matStarTextDefault;
            }
        }
    }

    public string GetMillisecString(float ms)
    {
        if(ms < 10)
        {
            return "00" + ms;
        }
        else if(ms < 100)
        {
            return "0" + ms;
        }

        return "" + ms;
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
