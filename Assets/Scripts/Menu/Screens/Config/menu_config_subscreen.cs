using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menu_config_subscreen : MonoBehaviour
{
    [SerializeField] Transform uiContainer;
    [SerializeField] protected TextMeshPro descTextEl;
    protected menu_config_button_type currentActiveType;

    private void Start()
    {
        this.enabled = false;
        //DisableScreen();
    }

    private void OnEnable()
    {
        uiContainer.gameObject.SetActive(true);
    }

    public virtual void HandleSelectionSwitch(menu_config_button_type type)
    {
        currentActiveType = type;
    }

    public virtual void HandleSelectionActivate()
    {

    }

    private void OnDisable()
    {
        DisableScreen();
    }

    protected virtual void DisableScreen()
    {
        currentActiveType = menu_config_button_type.UNDEFINED;
        if (uiContainer == null) return;
        uiContainer.gameObject.SetActive(false);
    }
}
