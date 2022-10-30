using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_worldselect : MonoBehaviour
{
    [SerializeField] private List<menu_worldinfo> worldInfoList;
    Vector2 elementScaleDefault, elementScaleActive;
    [SerializeField] Transform elementContainer;
    int currentlySelectedWorldIDX;
    int newSelectedWorldIXD; // used for transition

    [SerializeField] menu_navigate navigator;
    [SerializeField] Transform camTrans;

    private float selectMoveOffsetX;
    private float enterCamOffsetY = -54;

    [SerializeField] float transitionSpeedSwitch, transitionSpeedEnter;
    [SerializeField] AnimationCurve animCurve;
    float currentTransitionFactor;
    Vector2 elementContainerPosFrom, elementContainerPosTo;
    bool inSwitchTransition, inEnterTransition;

    private void Start()
    {
        navigator.enabled = false;

        elementScaleActive = worldInfoList[0].selectElement.transform.localScale;
        elementScaleDefault = worldInfoList[1].selectElement.transform.localScale;
        selectMoveOffsetX = elementScaleDefault.x * 2;
        //InitSelectionSwitch(-1);
    }

    private void Update()
    {
        if(inSwitchTransition)
        {
            HandleSwitchTransition();
            return;
        }

        if(inEnterTransition)
        {
            HandleEnterTransition();
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            EnterWorld(worldInfoList[currentlySelectedWorldIDX]);
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        if (inputX != 0) InitSelectionSwitch((int)inputX);

    }

    private void InitSelectionSwitch(int switchDirection)
    {
        if (currentlySelectedWorldIDX + switchDirection < 0 || currentlySelectedWorldIDX + switchDirection == worldInfoList.Count) return;
        //worldInfoList[currentlySelectedWorldIDX].selectElement.transform.localScale = elementScaleDefault;

        newSelectedWorldIXD += switchDirection;

        //worldInfoList[currentlySelectedWorldIDX].selectElement.transform.localScale = elementScaleActive;

        //elementContainer.localPosition = new Vector2(elementContainer.localPosition.x + (selectMoveOffsetX * -switchDirection), elementContainer.localPosition.y);
        elementContainerPosFrom = elementContainer.localPosition;
        elementContainerPosTo = new Vector2(elementContainer.localPosition.x + (selectMoveOffsetX * -switchDirection), elementContainer.localPosition.y);
        currentTransitionFactor = 0;
        inSwitchTransition = true;
    }

    private void HandleSwitchTransition()
    {
        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, 1, transitionSpeedSwitch * Time.deltaTime);

        worldInfoList[currentlySelectedWorldIDX].selectElement.transform.localScale = Vector2.Lerp(
            elementScaleActive,
            elementScaleDefault,
            //animCurve.Evaluate(currentTransitionFactor)
            currentTransitionFactor
            );

        worldInfoList[newSelectedWorldIXD].selectElement.transform.localScale = Vector2.Lerp(
            elementScaleDefault,
            elementScaleActive,
            //animCurve.Evaluate(currentTransitionFactor)
            currentTransitionFactor
            );

        elementContainer.localPosition = Vector2.Lerp(
            elementContainerPosFrom,
            elementContainerPosTo,
            //animCurve.Evaluate(currentTransitionFactor)
            currentTransitionFactor
            );

        // ugly but whtv
        if(currentTransitionFactor > 0.49f && currentTransitionFactor < 0.51f)
        {
            worldInfoList[currentlySelectedWorldIDX].text.SetActive(false);
            worldInfoList[newSelectedWorldIXD].text.SetActive(true);
        }

        if (currentTransitionFactor == 1)
        {
            inSwitchTransition = false;
            currentlySelectedWorldIDX = newSelectedWorldIXD;
        }
    }

    private void HandleEnterTransition()
    {
        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, 1, transitionSpeedEnter * Time.deltaTime);

        camTrans.localPosition = Vector3.Lerp(
            new Vector3(0, 0, -10),
            new Vector3(0, enterCamOffsetY, -10),
            animCurve.Evaluate(currentTransitionFactor)
            );

        if(currentTransitionFactor == 1)
        {
            inEnterTransition = false;
            navigator.enabled = true;
            this.enabled = false;
        }
    }

    private void EnterWorld(menu_worldinfo worldToEnter)
    {
        worldToEnter.levelSelectContainer.SetActive(true);
        navigator.currentSelection = worldToEnter.initSelectable;
        currentTransitionFactor = 0;
        inEnterTransition = true;
    }

}

[System.Serializable]
public class menu_worldinfo
{
    public GameObject selectElement;
    public GameObject text;
    public GameObject levelSelectContainer;
    public menu_selectable initSelectable;
}
