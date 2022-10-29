using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_navigate : MonoBehaviour
{
    public menu_selectable currentSelection;

    private bool canSwitch;
    private float switchCooldown = 0.14f;

    private void Start()
    {
        SwitchSelection(currentSelection);
        canSwitch = true;
    }

    void Update()
    {
        if (!canSwitch) return;

        float inputX = Input.GetAxisRaw("Horizontal");
        if (inputX == 1)
        {
            SwitchSelection(currentSelection.connectorRight);
            return;
        }
        if (inputX == -1)
        {
            SwitchSelection(currentSelection.connectorLeft);
            return;
        }

        float inputY = Input.GetAxisRaw("Vertical");
        if (inputY == 1)
        {
            SwitchSelection(currentSelection.connectorTop);
            return;
        }
        if (inputY == -1)
        {
            SwitchSelection(currentSelection.connectorBottom);
            return;
        }
    }

    protected void SwitchSelection(menu_selectable newSelection)
    {
        if (newSelection == null) return;

        currentSelection.enabled = false;
        currentSelection = newSelection;
        currentSelection.enabled = true;

        StartCoroutine(HandleCooldown());
    }

    private IEnumerator HandleCooldown()
    {
        canSwitch = false;
        yield return new WaitForSeconds(switchCooldown);
        canSwitch = true;
    }
}
