using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_config_display : menu_config_subscreen
{
    [SerializeField] SO_config configData;
    [SerializeField] string fullscreenDescText;
    [SerializeField] GameObject fullscreenToggleVisual;

    menu_config_display_button_type currentActiveType;

    private void Awake()
    {
        Screen.fullScreen = configData.fullscreen;
        fullscreenToggleVisual.SetActive(configData.fullscreen);
    }

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
                configData.fullscreen = !configData.fullscreen;
                Screen.fullScreen = configData.fullscreen;
                //fullscreenToggleVisual.SetActive(Screen.fullScreen);
                //fullscreenToggleVisual.SetActive(!fullscreenToggleVisual.activeSelf);
                fullscreenToggleVisual.SetActive(configData.fullscreen);
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
