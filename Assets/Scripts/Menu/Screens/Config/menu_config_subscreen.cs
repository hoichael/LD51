using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_config_subscreen : MonoBehaviour
{
    [SerializeField] Transform uiContainer;
    menu_config_button_type currentActiveType;

    private void Start()
    {
        this.enabled = false;
        //DisableScreen();
    }

    private void OnEnable()
    {
        uiContainer.gameObject.SetActive(true);
    }

    public void HandleSelectionSwitch(menu_config_button_type type)
    {
        currentActiveType = type;
    }

    private void OnDisable()
    {
        DisableScreen();
    }

    private void DisableScreen()
    {
        currentActiveType = menu_config_button_type.UNDEFINED;
        if (uiContainer == null) return;
        uiContainer.gameObject.SetActive(false);
    }
}
