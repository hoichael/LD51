using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_manager : MonoBehaviour
{
    [SerializeField] menu_screen_base initScreen;
    [SerializeField] menu_screen_switch screenSwitcher;
    menu_screen_base currentScreen;
    menu_screen_base screenToSwitchTo;
    bool currentlySwitching;

    private void Awake()
    {
        currentScreen = initScreen;
    }

    public void SwitchScreen(menu_screen_base newScreen)
    {
        if (currentlySwitching) return;
        currentlySwitching = true;

        currentScreen.OnSwitchFromInit();
        screenToSwitchTo = newScreen;
        screenToSwitchTo.OnSwitchToInit();

        screenSwitcher.Init(newScreen);
    }

    public void OnSwitchCompletion() // "callback" fired from menu_screen_switch
    {
        currentlySwitching = false;

        currentScreen.OnSwitchFromComplete();
        screenToSwitchTo.OnSwitchToComplete();

        currentScreen = screenToSwitchTo;
        screenToSwitchTo = null;
    }
}
