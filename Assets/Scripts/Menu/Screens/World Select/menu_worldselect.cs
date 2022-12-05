using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_worldselect : menu_screen_base
{
    [SerializeField] menu_manager menuManager;
    [SerializeField] SO_pd_session sessionData;
    [SerializeField] InputServer input;
    [SerializeField] menu_main mainMenuManager;
    [SerializeField] menu_lvselect_manager lvSelectManager;
    [SerializeField] List<menu_worldselectinfo> worldInfoList;
    Vector2 elementScaleDefault, elementScaleActive;
    [SerializeField] Transform elementContainer;
    int currentlySelectedWorldIDX;
    int newSelectedWorldIDX;

    [SerializeField] menu_navigate navigator;
    [SerializeField] Transform camTrans;

    float selectMoveOffsetX;

    [SerializeField] float transitionSpeedSwitch, transitionSpeedEnter;
    [SerializeField] AnimationCurve animCurve;
    float currentTransitionFactor;
    Vector2 elementContainerPosFrom, elementContainerPosTo;
    bool inSwitchTransition;

    private void Awake()
    {
        navigator.enabled = false;

        elementScaleActive = worldInfoList[0].selectElement.transform.localScale;
        elementScaleDefault = worldInfoList[1].selectElement.transform.localScale;
        selectMoveOffsetX = elementScaleDefault.x * 2;
    }

    private void Update()
    {
        if(inSwitchTransition)
        {
            HandleSwitchTransition();
            return;
        }

        if(input.I.Menu.Exit.WasPressedThisFrame())
        {
            menuManager.SwitchScreen(mainMenuManager);
        }

        if(input.I.Menu.Enter.WasPressedThisFrame())
        {
            sessionData.selectedWorldIDX = currentlySelectedWorldIDX;
            menuManager.SwitchScreen(lvSelectManager);
        }

        if (input.I.Menu.Right.IsPressed())
        {
            InitSelectionSwitch(1);
            return;
        }
        else if (input.I.Menu.Left.IsPressed())
        {
            InitSelectionSwitch(-1);
        }

    }

    private void InitSelectionSwitch(int switchDirection)
    {
        if (currentlySelectedWorldIDX + switchDirection < 0 || currentlySelectedWorldIDX + switchDirection == worldInfoList.Count) return;

        newSelectedWorldIDX += switchDirection;

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
            currentTransitionFactor
            );

        worldInfoList[newSelectedWorldIDX].selectElement.transform.localScale = Vector2.Lerp(
            elementScaleDefault,
            elementScaleActive,
            currentTransitionFactor
            );

        elementContainer.localPosition = Vector2.Lerp(
            elementContainerPosFrom,
            elementContainerPosTo,
            currentTransitionFactor
            );

        // ugly but whtv
        if(currentTransitionFactor > 0.49f && currentTransitionFactor < 0.51f)
        {
            worldInfoList[currentlySelectedWorldIDX].text.SetActive(false);
            worldInfoList[newSelectedWorldIDX].text.SetActive(true);
        }

        if (currentTransitionFactor == 1)
        {
            inSwitchTransition = false;
            currentlySelectedWorldIDX = newSelectedWorldIDX;
        }
    }

    public override void OnSwitchFromInit()
    {
        base.OnSwitchFromInit();
        this.enabled = false;
    }

    public override void OnSwitchToComplete()
    {
        base.OnSwitchToComplete();
        this.enabled = true;
    }
}

[System.Serializable]
public class menu_worldselectinfo
{
    public GameObject selectElement;
    public GameObject text;
}
