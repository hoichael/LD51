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

        screenToSwitchTo = newScreen;
        screenToSwitchTo.OnSwitchToInit();
        currentScreen.OnSwitchFromInit();

        screenSwitcher.Init(newScreen);
    }

    public void OnSwitchCompletion() // "callback" fired from menu_screen_switch
    {
        currentlySwitching = false;

        screenToSwitchTo.OnSwitchToComplete();
        currentScreen.OnSwitchFromComplete();

        currentScreen = screenToSwitchTo;
        screenToSwitchTo = null;
    }
}
