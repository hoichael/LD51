using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_config_display : menu_config_subscreen
{
    [SerializeField] string fullscreenDescText;

    public override void HandleSelectionSwitch(menu_config_button_type type)
    {
        base.HandleSelectionSwitch(type);

        switch (type)
        {
            case menu_config_button_type.Fullscreen:
                descTextEl.text = fullscreenDescText;
                break;
            case menu_config_button_type.UNDEFINED:

                break;
            default:

                break;
        }

    }

    public override void HandleSelectionActivate()
    {
        base.HandleSelectionActivate();

        switch (currentActiveType)
        {
            case menu_config_button_type.Fullscreen:
                print("FULLSCREEN TOGGLE PRESSED");
                break;
            case menu_config_button_type.UNDEFINED:

                break;
            default:

                break;
        }

    }
}
