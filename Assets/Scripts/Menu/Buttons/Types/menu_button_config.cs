using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menu_button_config : menu_selectable
{
    [SerializeField] TextMeshPro textElement;
    [SerializeField] menu_config configManager;
    [SerializeField] menu_config_button_type buttonType;
    [SerializeField] Color colorDefault, colorActive;

    private void Awake()
    {
        //textElement.color = colorDefault;
    }

    protected override void Enter()
    {
        base.Enter();
        textElement.color = colorActive;

        if (buttonType == menu_config_button_type.UNDEFINED)
        {
            print("UNDEFINED BUTTON TYPE in CONFIG MENU");
            return;
        }
        configManager.HandleButtonSwitch(buttonType);
    }

    protected override void Exit()
    {
        base.Exit();
        textElement.color = colorDefault;
    }
}
