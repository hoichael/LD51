using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_worldselect : MonoBehaviour
{
    [SerializeField] private List<menu_worldinfo> worldInfoList;
    Vector2 elementScaleDefault, elementScaleActive;
    [SerializeField] Transform elementContainer;
    int currentlySelectedWorldIDX;

    [SerializeField] menu_navigate navigator;
    [SerializeField] Transform camTrans;

    private float selectMoveOffsetX;
    private float enterCamOffsetY = -54;

    bool canSwitch = true;

    private void Start()
    {
        navigator.enabled = false;

        elementScaleActive = worldInfoList[0].selectElement.transform.localScale;
        elementScaleDefault = worldInfoList[1].selectElement.transform.localScale;
        selectMoveOffsetX = elementScaleDefault.x * 2;
        SwitchSelection(-1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            EnterWorld(worldInfoList[currentlySelectedWorldIDX]);
        }

        if (!canSwitch) return;

        float inputX = Input.GetAxisRaw("Horizontal");
        if (inputX != 0) SwitchSelection((int)inputX);

    }

    private void SwitchSelection(int switchDirection)
    {
        if (currentlySelectedWorldIDX + switchDirection < 0 || currentlySelectedWorldIDX + switchDirection == worldInfoList.Count) return;
        worldInfoList[currentlySelectedWorldIDX].selectElement.transform.localScale = elementScaleDefault;
        currentlySelectedWorldIDX += switchDirection;
        worldInfoList[currentlySelectedWorldIDX].selectElement.transform.localScale = elementScaleActive;

        elementContainer.localPosition = new Vector2(elementContainer.localPosition.x + (selectMoveOffsetX * -switchDirection), elementContainer.localPosition.y);

        StartCoroutine(HandleCooldown());
    }

    private void EnterWorld(menu_worldinfo worldToEnter)
    {
        navigator.currentSelection = worldToEnter.initSelectable;
        worldToEnter.levelSelectContainer.SetActive(true);

        camTrans.localPosition = new Vector3(0, enterCamOffsetY, -10);

        navigator.enabled = true;
        this.enabled = false;
    }

    private IEnumerator HandleCooldown()
    {
        canSwitch = false;
        yield return new WaitForSeconds(0.12f);
        canSwitch = true;
    }

}

[System.Serializable]
public class menu_worldinfo
{
    public GameObject selectElement;
    public GameObject levelSelectContainer;
    public menu_selectable initSelectable;
}
