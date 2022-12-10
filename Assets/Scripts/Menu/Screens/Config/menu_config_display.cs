using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_config_display : menu_config_subscreen
{
    [SerializeField] string fullscreenDescText;
    [SerializeField] GameObject fullscreenToggleVisual;

    menu_config_display_button_type currentActiveType;

    public void HandleSelectionSwitch(menu_config_display_button_type type)
    {

        switch (type)
        {
            case menu_config_display_button_type.Fullscreen:
                descTextEl.text = fullscreenDescText;
                currentActiveType = type;
                break;
            case menu_config_display_button_type.UNDEFINED:

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
            case menu_config_display_button_type.Fullscreen:
                Screen.fullScreen = !Screen.fullScreen;
                //fullscreenToggleVisual.SetActive(Screen.fullScreen);
                fullscreenToggleVisual.SetActive(!fullscreenToggleVisual.activeSelf);
                break;
            case menu_config_display_button_type.UNDEFINED:

                break;
            default:

                break;
        }

    }
}

public enum menu_config_display_button_type
{
    UNDEFINED,
    Fullscreen
}
