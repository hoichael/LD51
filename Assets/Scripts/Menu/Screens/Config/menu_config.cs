using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_config : menu_screen_base
{
    [SerializeField] InputServer input;
    [SerializeField] menu_navigate navigator;
    [SerializeField] menu_selectable initSelectedButton;
    [SerializeField] menu_main mainMenuManager;
    [SerializeField] menu_config_subscreen subscreenAudio, subscreenDisplay, subscreenControls;

    Dictionary<menu_config_button_type, menu_config_subscreen> subscreenDict;
    menu_config_subscreen currentActiveSubscreen;

    private void Awake()
    {
        SetupData();
        currentActiveSubscreen = subscreenAudio;
        this.enabled = false;
    }

    public override void OnSwitchToInit()
    {
        base.OnSwitchToInit();
        //navigator.currentSelection = initSelectedButton;
        navigator.enabled = true;
        navigator.SwitchSelection(initSelectedButton);
        this.enabled = true;
    }

    public override void OnSwitchFromInit()
    {
        base.OnSwitchFromInit();
        navigator.enabled = false;
        this.enabled = false;
    }

    public void HandleButtonSwitch(menu_config_button_type type)
    {
        if(type == menu_config_button_type.SelectAudio ||
           type == menu_config_button_type.SelectDisplay ||
           type == menu_config_button_type.SelectControls)
        {
            SwitchSubscreen(type);
        }
        else
        {
            
        }
    }

    private void Update()
    {
        if (input.I.Menu.Exit.WasPressedThisFrame())
        {
            menuManager.SwitchScreen(mainMenuManager);
        }
    }

    private void SwitchSubscreen(menu_config_button_type newSubscreenType)
    {
        if (subscreenDict == null) return; // idc
        if (ReferenceEquals(subscreenDict[newSubscreenType], currentActiveSubscreen)) return;
        currentActiveSubscreen.enabled = false;
        currentActiveSubscreen = subscreenDict[newSubscreenType];
        currentActiveSubscreen.enabled = true;
    }

    private void SetupData()
    {
        subscreenDict = new Dictionary<menu_config_button_type, menu_config_subscreen>();
        subscreenDict.Add(menu_config_button_type.SelectAudio, subscreenAudio);
        subscreenDict.Add(menu_config_button_type.SelectDisplay, subscreenDisplay);
        subscreenDict.Add(menu_config_button_type.SelectControls, subscreenControls);
    }
}

public enum menu_config_button_type
{
    UNDEFINED,

    SelectAudio,
    SelectDisplay,
    SelectControls,

    VolumeMaster,
    VolumeMusic,
    VolumeSFX,

    Fullscreen
}