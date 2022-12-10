using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menu_button_config_display : menu_selectable
{
    [SerializeField] menu_config_display_button_type type;

    [SerializeField] TextMeshPro textElement;
    [SerializeField] menu_config_display configDisplayManager;
    [SerializeField] Color colorDefault, colorActive;

    protected override void Enter()
    {
        base.Enter();
        textElement.color = colorActive;

        if (type == menu_config_display_button_type.UNDEFINED)
        {
            print("UNDEFINED BUTTON TYPE in CONFIG MENU");
            return;
        }
        configDisplayManager.HandleSelectionSwitch(type);
    }

    public override void Activate()
    {
        base.Activate();

        if (type == menu_config_display_button_type.UNDEFINED)
        {
            print("UNDEFINED BUTTON TYPE in CONFIG MENU");
            return;
        }
        configDisplayManager.HandleSelectionActivate();
    }

    protected override void Exit()
    {
        base.Exit();
        textElement.color = colorDefault;
    }
}
