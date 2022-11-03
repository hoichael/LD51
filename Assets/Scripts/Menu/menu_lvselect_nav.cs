using System.Collections;
using UnityEngine;

public class menu_lvselect_nav : MonoBehaviour
{
    [SerializeField] menu_lvselect_manager manager;
    [SerializeField] menu_lvselect_settings settings;
    [SerializeField] InputServer input;
    int currentSelectionIDX;
    public SpriteRenderer[] sprArr;
    bool canSwitch;

    private void OnEnable()
    {
        canSwitch = true;
        SwitchSelection(0); // script needs to be disabled by default, on inspector level
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!canSwitch) return;

        if (input.I.Menu.Left.IsPressed())
        {
            SwitchSelection(currentSelectionIDX - 1);
            return;
        }
        if (input.I.Menu.Right.IsPressed())
        {
            SwitchSelection(currentSelectionIDX + 1);
            return;
        }
        if (input.I.Menu.Down.IsPressed())
        {
            SwitchSelection(currentSelectionIDX + settings.lvElRowSize);
            return;
        }
        if (input.I.Menu.Up.IsPressed())
        {
            SwitchSelection(currentSelectionIDX - settings.lvElRowSize);
        }
    }

    private void SwitchSelection(int newSelectionIDX)
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

        sprArr[currentSelectionIDX].sprite = settings.sprIconDefault;

        currentSelectionIDX = newIDX;
        sprArr[currentSelectionIDX].sprite = settings.sprIconActive;

        manager.HandleSelectionSwitch(currentSelectionIDX);

        StartCoroutine(HandleCooldown());
    }

    private IEnumerator HandleCooldown()
    {
        canSwitch = false;
        yield return new WaitForSeconds(settings.selectionCooldown);
        canSwitch = true;
    }
}
