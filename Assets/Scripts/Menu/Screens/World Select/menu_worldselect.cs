using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_worldselect : MonoBehaviour
{
    [SerializeField] InputServer input;
    [SerializeField] menu_lvselect_manager lvSelectManager;
    [SerializeField] List<menu_worldselectinfo> worldInfoList;
    Vector2 elementScaleDefault, elementScaleActive;
    [SerializeField] Transform elementContainer;
    int currentlySelectedWorldIDX;
    int newSelectedWorldIDX; // used for transition

    [SerializeField] menu_navigate navigator;
    [SerializeField] Transform camTrans;

    float selectMoveOffsetX;
    float enterCamOffsetY = -54;

    [SerializeField] float transitionSpeedSwitch, transitionSpeedEnter;
    [SerializeField] AnimationCurve animCurve;
    float currentTransitionFactor;
    Vector2 elementContainerPosFrom, elementContainerPosTo;
    bool inSwitchTransition, inEnterTransition;

    float enterTransitionTarget;

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

        //if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        if(input.I.Menu.Enter.WasPressedThisFrame())
        {
            EnterWorld(worldInfoList[currentlySelectedWorldIDX]);
        }

        //float inputX = Input.GetAxisRaw("Horizontal");
        //if (inputX != 0) InitSelectionSwitch((int)inputX);
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
        //worldInfoList[currentlySelectedWorldIDX].selectElement.transform.localScale = elementScaleDefault;

        newSelectedWorldIDX += switchDirection;

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

        worldInfoList[newSelectedWorldIDX].selectElement.transform.localScale = Vector2.Lerp(
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
            worldInfoList[newSelectedWorldIDX].text.SetActive(true);
        }

        if (currentTransitionFactor == 1)
        {
            inSwitchTransition = false;
            currentlySelectedWorldIDX = newSelectedWorldIDX;
        }
    }

    private void HandleEnterTransition()
    {
        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, enterTransitionTarget, transitionSpeedEnter * Time.deltaTime);

        camTrans.localPosition = Vector3.Lerp(
            new Vector3(0, 0, -10),
            new Vector3(0, enterCamOffsetY, -10),
            animCurve.Evaluate(currentTransitionFactor)
            );

        if(currentTransitionFactor == enterTransitionTarget)
        {
            inEnterTransition = false;
            if(enterTransitionTarget == 1)
            {
                //navigator.enabled = true;
                this.enabled = false;
            }
        }
    }

    private void EnterWorld(menu_worldselectinfo worldToEnter)
    {
        //worldToEnter.levelSelectContainer.SetActive(true);
        //navigator.currentSelection = worldToEnter.initSelectable;

        currentTransitionFactor = 0;
        enterTransitionTarget = 1;
        inEnterTransition = true;

        //lvSelectManager.Init(0);
        //lvSelectManager.Init(Mathf.Clamp(currentlySelectedWorldIDX, 0, 1));
        lvSelectManager.Init(currentlySelectedWorldIDX);
    }

    public void LeaveWorld()
    {
        currentTransitionFactor = 1;
        enterTransitionTarget = 0;
        inEnterTransition = true;
    }

}

[System.Serializable]
public class menu_worldselectinfo
{
    public GameObject selectElement;
    public GameObject text;
    //public GameObject levelSelectContainer;
    //public menu_selectable initSelectable;
}
