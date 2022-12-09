using UnityEngine;

public class menu_main : menu_screen_base
{
    [SerializeField] menu_navigate navigator;
    [SerializeField] menu_selectable initSelectedButton;
    [SerializeField] menu_worldselect worldSelectScreen;
    [SerializeField] menu_config configScreen;
    //menu_selectable currentSelection;

    private void Start()
    {
        navigator.currentSelection = initSelectedButton;
        navigator.enabled = true;
    }

    public override void OnSwitchToInit()
    {
        base.OnSwitchToInit();
        //navigator.currentSelection = currentSelection;
        //navigator.currentSelection = initSelectedButton;
        navigator.SwitchSelection(initSelectedButton);
        navigator.enabled = true;
        this.enabled = true;
    }

    public override void OnSwitchFromInit()
    {
        base.OnSwitchFromInit();
        navigator.enabled = false;
        this.enabled = false;
    }

    public void HandleButton(menu_main_button_type type) // called from menu_button_main
    {
        switch (type)
        {
            case menu_main_button_type.Play:
                menuManager.SwitchScreen(worldSelectScreen);
                break;
            case menu_main_button_type.Config:
                menuManager.SwitchScreen(configScreen);
                break;
            case menu_main_button_type.Exit:
                #if UNITY_STANDALONE
                    Application.Quit();
                #endif
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #endif
                break;
        }
    }
}

public enum menu_main_button_type
{
    UNDEFINED,
    Play,
    Config,
    Exit
}
