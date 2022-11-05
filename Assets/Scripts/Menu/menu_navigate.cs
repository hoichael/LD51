using System.Collections;
using UnityEngine;

public class menu_navigate : MonoBehaviour
{
    [SerializeField] private InputServer input;
    public menu_selectable currentSelection;

    bool canSwitch;
    float switchCooldown = 0.14f;

    private void OnEnable()
    {
        SwitchSelection(currentSelection);
        canSwitch = true;
    }

    void Update()
    {
        if (!canSwitch) return;

        if(input.I.Menu.Enter.WasPressedThisFrame())
        {
            if (currentSelection != null) currentSelection.Activate();
            return;
        }
        if (input.I.Menu.Right.IsPressed())
        {
            SwitchSelection(currentSelection.connectorRight);
            return;
        }
        if (input.I.Menu.Left.IsPressed())
        {
            SwitchSelection(currentSelection.connectorLeft);
            return;
        }
        if (input.I.Menu.Up.IsPressed())
        {
            SwitchSelection(currentSelection.connectorTop);
            return;
        }
        if (input.I.Menu.Down.IsPressed())
        {
            SwitchSelection(currentSelection.connectorBottom);
        }
    }

    public void SwitchSelection(menu_selectable newSelection)
    {
        if (newSelection == null) return;

        if(currentSelection != null)
        {
            currentSelection.enabled = false;
        }

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
