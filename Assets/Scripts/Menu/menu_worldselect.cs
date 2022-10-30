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

    [SerializeField] float transitionSpeed;
    [SerializeField] AnimationCurve animCurve;
    float currentTransitionFactor;
    Vector2 elementContainerPosFrom, elementContainerPosTo;
    bool inTransition;

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
        if(inTransition)
        {
            HandleTransition();
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
        inTransition = true;
    }

    private void HandleTransition()
    {
        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, 1, transitionSpeed * Time.deltaTime);

        worldInfoList[currentlySelectedWorldIDX].selectElement.transform.localScale = Vector2.Lerp(
            elementScaleActive,
            elementScaleDefault,
            animCurve.Evaluate(currentTransitionFactor)
            );

        worldInfoList[newSelectedWorldIXD].selectElement.transform.localScale = Vector2.Lerp(
            elementScaleDefault,
            elementScaleActive,
            animCurve.Evaluate(currentTransitionFactor)
            );

        elementContainer.localPosition = Vector2.Lerp(
            elementContainerPosFrom,
            elementContainerPosTo,
            animCurve.Evaluate(currentTransitionFactor)
            );

        if (currentTransitionFactor == 1)
        {
            inTransition = false;
            currentlySelectedWorldIDX = newSelectedWorldIXD;
        }
    }

    private void EnterWorld(menu_worldinfo worldToEnter)
    {
        navigator.currentSelection = worldToEnter.initSelectable;
        worldToEnter.levelSelectContainer.SetActive(true);

        camTrans.localPosition = new Vector3(0, enterCamOffsetY, -10);

        navigator.enabled = true;
        this.enabled = false;
    }

}

[System.Serializable]
public class menu_worldinfo
{
    public GameObject selectElement;
    public GameObject levelSelectContainer;
    public menu_selectable initSelectable;
}
