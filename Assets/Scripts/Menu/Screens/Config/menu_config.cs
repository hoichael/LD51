using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menu_config : menu_screen_base
{
    [SerializeField] InputServer input;
    [SerializeField] menu_navigate navigator;
    [SerializeField] menu_selectable initSelectedButton;
    [SerializeField] menu_main mainMenuManager;
    [SerializeField] TextMeshPro descTextEl;
    [SerializeField] string descDefaultText;
    [SerializeField] subscreen_scene_data subscreenDataAudio, subscreenDataDisplay, subscreenDataControls;

    Dictionary<menu_config_button_type, subscreen_scene_data> subscreenDict;
    menu_config_button_type currentSubscreenType;
    menu_config_subscreen currentActiveSubscreen;

    private void Awake()
    {
        SetupData();
        HandleInit();
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
        //if(type == menu_config_button_type.SelectAudio ||
        //   type == menu_config_button_type.SelectDisplay ||
        //   type == menu_config_button_type.SelectControls)
        //{
        //    SwitchSubscreen(type);

        //    descTextEl.text = descDefaultText; // technically doesnt belong here but works well as a side effect. will prob keep it here until it doesnt align with the program flow anymore
        //}
        ////else
        ////{
        ////    currentActiveSubscreen.HandleSelectionSwitch(type);
        ////}

        descTextEl.text = descDefaultText; // technically doesnt belong here but works well as a side effect. will prob keep it here until it doesnt align with the program flow anymore
        SwitchSubscreen(type);
    }

    public void HandleButtonPress()
    {
        //print("CONFIG BUTTON PRESS");
        currentActiveSubscreen.HandleSelectionActivate();
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
        //if (ReferenceEquals(subscreenDict[newSubscreenType], currentActiveSubscreen)) return;
        if (newSubscreenType == currentSubscreenType) return;

        currentActiveSubscreen.enabled = false;
        subscreenDict[currentSubscreenType].buttonIndicator.gameObject.SetActive(false);

        currentSubscreenType = newSubscreenType;
        currentActiveSubscreen = subscreenDict[currentSubscreenType].subscreenManager;
        subscreenDict[currentSubscreenType].buttonIndicator.gameObject.SetActive(true);
        currentActiveSubscreen.enabled = true;
    }

    private void SetupData()
    {
        subscreenDict = new Dictionary<menu_config_button_type, subscreen_scene_data>();
        subscreenDict.Add(menu_config_button_type.SelectAudio, subscreenDataAudio);
        subscreenDict.Add(menu_config_button_type.SelectDisplay, subscreenDataDisplay);
        subscreenDict.Add(menu_config_button_type.SelectControls, subscreenDataControls);
    }

    private void HandleInit()
    {
        foreach(KeyValuePair<menu_config_button_type, subscreen_scene_data> entry in subscreenDict)
        {
            entry.Value.buttonIndicator.gameObject.SetActive(false);
        }

        // initializing to controls subscreen instead of audio subscreen because fuckery. whtv no biggie
        currentActiveSubscreen = subscreenDataControls.subscreenManager;
        currentSubscreenType = menu_config_button_type.SelectControls;
        subscreenDataControls.buttonIndicator.gameObject.SetActive(true);
        currentActiveSubscreen.enabled = true;

        //descDefaultText = descTextEl.text;
        descTextEl.text = descDefaultText;

        this.enabled = false;
    }
}

public enum menu_config_button_type
{
    UNDEFINED,

    SelectAudio,
    SelectDisplay,
    SelectControls,

    //VolumeMaster,
    //VolumeMusic,
    //VolumeSFX,

    //Fullscreen
}

[System.Serializable]
public class subscreen_scene_data
{
    public menu_config_subscreen subscreenManager;
    public Transform buttonIndicator;
}