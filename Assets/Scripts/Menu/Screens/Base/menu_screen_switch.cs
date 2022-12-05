using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_screen_switch : MonoBehaviour
{
    [SerializeField] menu_settings settings;
    [SerializeField] Transform camTrans;
    [SerializeField] menu_manager menuManager;
    Vector3 currentCamPos;
    Vector3 currentCamTarget;
    bool currentlyInTransition;
    float transitionFactor;

    public void Init(menu_screen_base newScreen)
    {
        currentCamPos = camTrans.position;
        transitionFactor = 0;
        currentCamTarget = new Vector3(newScreen.uiContainer.position.x, newScreen.uiContainer.position.y, settings.camPosZ);
        currentlyInTransition = true;
    }

    private void Update()
    {
        if (!currentlyInTransition) return;
        HandleTransition();
    }

    private void HandleTransition()
    {
        transitionFactor = Mathf.MoveTowards(transitionFactor, 1, settings.screenSwitchSpeed * Time.deltaTime);
        
        camTrans.position = Vector3.Lerp(
            currentCamPos,
            currentCamTarget,
            settings.screenSwitchCurve.Evaluate(transitionFactor)
            );

        if(transitionFactor == 1)
        {
            currentlyInTransition = false;
            menuManager.OnSwitchCompletion();
        }
    }
}
