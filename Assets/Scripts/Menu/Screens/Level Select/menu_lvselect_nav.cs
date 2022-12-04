using System.Collections;
using UnityEngine;

public class menu_lvselect_nav : MonoBehaviour
{
    [SerializeField] menu_lvselect_manager manager;
    [SerializeField] menu_lvselect_data data;
    [SerializeField] InputServer input;
    int currentSelectionIDX;
    public SpriteRenderer[] sprArr;
    bool canSwitch;

    private void OnEnable()
    {
        canSwitch = true;
        SwitchSelection(0, true); // script needs to be disabled by default, on inspector level
    }
    private void OnDisable()
    {
        // check for scene switch fuckery (enter level). pretty ugly but no easy way around this.
        if(sprArr != null && sprArr[currentSelectionIDX] != null && sprArr[currentSelectionIDX].sprite != null)
        {
            sprArr[currentSelectionIDX].sprite = data.sprIconDefault;
        }
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!canSwitch) return;

        if (input.I.Menu.Enter.WasPressedThisFrame())
        {
            manager.EnterLevel(currentSelectionIDX);
        }

        if (input.I.Menu.Left.IsPressed())
        {
            SwitchSelection(currentSelectionIDX - 1, false);
            return;
        }
        if (input.I.Menu.Right.IsPressed())
        {
            SwitchSelection(currentSelectionIDX + 1, false);
            return;
        }
        if (input.I.Menu.Down.IsPressed())
        {
            SwitchSelection(currentSelectionIDX + data.lvElRowSize, false);
            return;
        }
        if (input.I.Menu.Up.IsPressed())
        {
            SwitchSelection(currentSelectionIDX - data.lvElRowSize, false);
        }
    }

    private void SwitchSelection(int newSelectionIDX, bool fromInit /* yes */)
    {
        int newIDX = newSelectionIDX;

        if (newIDX == sprArr.Length)
        {
            newIDX = 0;
        }
        else if (newIDX == -1)
        {
            newIDX = sprArr.Length - 1;
        }
        else if (newIDX > sprArr.Length || newIDX < 0)
        {
            return;
        }

        if(!fromInit)
        {
            sprArr[currentSelectionIDX].sprite = data.sprIconDefault;
        }

        currentSelectionIDX = newIDX;
        sprArr[currentSelectionIDX].sprite = data.sprIconActive;

        manager.HandleSelectionSwitch(currentSelectionIDX);

        StartCoroutine(HandleCooldown());
    }

    private IEnumerator HandleCooldown()
    {
        canSwitch = false;
        yield return new WaitForSeconds(data.selectionCooldown);
        canSwitch = true;
    }
}
